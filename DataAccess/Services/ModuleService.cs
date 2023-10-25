using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IModuleService : IBaseService<Module>
    {
        Task SetAssignmentStatus(BaseModel model);
        List<GetSubmissionsResponse> GetSubmissions(GetSubmissionsRequest model);
        List<ListSubmittedPAResponse> GetListSubmittedPA(Guid idStudent);
        List<ListSubmittedJResponse> GetListSubmittedJ(Guid idStudent);
    }
    public class ModuleService : BaseService<Module>, IModuleService
    {
        public ModuleService(AppDbContext appDbContext) : base(appDbContext) { }
        public List<ListSubmittedPAResponse> GetListSubmittedPA(Guid idStudent)
        {
            List<ListSubmittedPAResponse> models = new();
            List<Module> entities;
            entities = _appDbContext.Set<Module>().Include(x => x.PAAnswers).AsNoTracking().OrderBy(x => x.SeelabsId).ToList();

            if (entities != null)
                foreach (var entity in entities)
                {
                    ListSubmittedPAResponse model = new();
                    model.MapToModel(entity);
                    model.IsSubmitted = entity.PAAnswers.Any(x => x.IdModule == entity.Id && x.IdStudent == idStudent);
                    models.Add(model);
                }
            return models;
        }
        public List<ListSubmittedJResponse> GetListSubmittedJ(Guid idStudent)
        {
            List<ListSubmittedJResponse> models = new();
            List<Module> entities;
            entities = _appDbContext.Set<Module>().Include(x => x.JAnswers).AsNoTracking().OrderBy(x => x.SeelabsId).ToList();

            if (entities != null)
                foreach (var entity in entities)
                {
                    ListSubmittedJResponse model = new();
                    model.MapToModel(entity);
                    model.IsSubmitted = entity.JAnswers.Any(x => x.IdModule == entity.Id && x.IdStudent == idStudent);
                    models.Add(model);
                }
            return models;
        }
        public async Task SetAssignmentStatus(BaseModel model)
        {
            var module = _appDbContext.Set<Module>();
            if (model is SetPAStatusRequest pa)
            {
                Module entity = await module.FindAsync(pa.Id);
                entity.IsPAOpen = pa.IsOpen;
            }
            else if (model is SetPRTStatusRequest prt)
            {
                Module entity = await module.FindAsync(prt.Id);
                entity.IsPRTOpen = prt.IsOpen;
            }
            await _appDbContext.SaveChangesAsync();
        }
        public List<GetSubmissionsResponse> GetSubmissions(GetSubmissionsRequest request)
        {
            List<GetSubmissionsResponse> models = new();
            List<Student> students = _appDbContext.Set<Student>().Include(x => x.User).Where(x => x.Group == request.Group).OrderBy(x => x.User.Name).AsNoTracking().ToList();
            Module module = _appDbContext.Set<Module>()
                            .Include(j => j.JAnswers).ThenInclude(s => s.Student)
                            .Include(pa => pa.PAAnswers).ThenInclude(s => s.Student)
                            .AsNoTracking()
                            .FirstOrDefault(x => x.SeelabsId == request.SeelabsId);
            foreach (var student in students)
            {
                GetSubmissionsResponse model = new();
                model.MapToModel(student);
                model.MapToModel(student.User);
                model.MapToModel(module);
                model.MapToModel(module.PAAnswers.FirstOrDefault(x => x.IdStudent == student.Id));
                model.MapToModel(module.JAnswers.FirstOrDefault(x => x.IdStudent == student.Id));
                models.Add(model);
            }
            return models;
        }
    }
}
