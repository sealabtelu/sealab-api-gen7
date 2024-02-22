using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SealabAPI.DataAccess;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;
using SealabAPI.DataAccess.Models.Constants;
using Microsoft.AspNetCore.Authorization;

namespace SealabAPI.Base
{
    public class BaseController<
        ModelCreate,
        ModelUpdate,
        ModelDetail,
        TEntity> : ControllerBase
        where ModelCreate : BaseModel
        where ModelUpdate : BaseModel
        where ModelDetail : BaseModel, new()
        where TEntity : BaseEntity
    {
        protected IBaseService<TEntity> _baseService;
        public BaseController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }
        [HttpPost]
        public virtual async Task<ActionResult> Create(ModelCreate model)
        {
            try
            {
                TEntity result = await _baseService.Create(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), result.Id);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        [HttpPut]
        public virtual async Task<ActionResult> Update(ModelUpdate model)
        {
            try
            {
                var result = await _baseService.Update(model);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), result.Id);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        [HttpDelete]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _baseService.Delete(id);

                return new SuccessApiResponse(string.Format(MessageConstant.Success), id);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<ModelDetail>> GetById(Guid id)
        {
            try
            {
                ModelDetail model = await _baseService.Get<ModelDetail>(x => x.Id == id);
                return new SuccessApiResponse(string.Format(MessageConstant.Success), model);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        [HttpGet("list")]
        public virtual ActionResult<List<ModelDetail>> GetList()
        {
            try
            {
                List<ModelDetail> models = _baseService.GetAll<ModelDetail>();
                return new SuccessApiResponse(string.Format(MessageConstant.Success), models);
            }
            catch (Exception ex)
            {
                return new ErrorApiResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
    }

    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            return value == null ? null :
                   Regex.Replace(value.ToString()!,
                   "([a-z])([A-Z])", "$1-$2",
                   RegexOptions.CultureInvariant,
                   TimeSpan.FromMilliseconds(100)).ToLowerInvariant();
        }
    }

    public class UtcDateTimeConverterHelper : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var stringValue = jsonDoc.RootElement.GetRawText().Trim('"').Trim('\'');
            var value = DateTime.Parse(stringValue, null, System.Globalization.DateTimeStyles.RoundtripKind);
            return value;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture));
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class StudentRestrictedAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            AppDbContext appDbContext = context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();
            if (!StudentIsAllowed(context.HttpContext, appDbContext))
            {
                context.Result = new ForbidResult();
            }
        }

        private bool StudentIsAllowed(HttpContext context, AppDbContext appDbContext)
        {
            Guid idUser = Guid.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string role = context.User.FindFirst(ClaimTypes.Role)?.Value;

            if (role == "Student")
            {
                Student student = appDbContext.Set<Student>().FirstOrDefault(u => u.IdUser == idUser);
                TimeSpan start = new(6 + 3 * (student.Shift - 1), 30, 0);
                TimeSpan end = new(9 + 3 * (student.Shift - 1), 0, 0);
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                return student.Day == (int)DateTime.Now.DayOfWeek &&
                       currentTime >= start && currentTime <= end;
            }
            return true;
        }
    }
}

