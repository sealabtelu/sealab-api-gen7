using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IModuleService : IBaseService<Module>
    {
        Task<List<DetailModuleResponse>> SetAssignmentStatus(BaseModel model);
        List<GetSubmissionsResponse> GetSubmissions(GetSubmissionsRequest model);
        List<ListSubmittedPAResponse> GetListSubmittedPA(Guid idStudent);
        List<ListSubmittedPRTResponse> GetListSubmittedPRT(Guid idStudent);
        List<ListSubmittedJResponse> GetListSubmittedJ(Guid idStudent);
        List<ListSubmittedFTResponse> GetListSubmittedFT(Guid idStudent);
        Task DeleteAllModules();
    }
    public class ModuleService : BaseService<Module>, IModuleService
    {
        public ModuleService(AppDbContext appDbContext) : base(appDbContext) { }
        public List<ListSubmittedPAResponse> GetListSubmittedPA(Guid idStudent)
        {
            List<ListSubmittedPAResponse> models = new();
            List<Module> entities = _appDbContext.Set<Module>().Include(x => x.PAAnswers).AsNoTracking().OrderBy(x => x.SeelabsId).ToList();

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
        public List<ListSubmittedPRTResponse> GetListSubmittedPRT(Guid idStudent)
        {
            List<ListSubmittedPRTResponse> models = new();
            List<Module> entities = _appDbContext.Set<Module>().AsNoTracking().OrderBy(x => x.SeelabsId).ToList();
            List<PreTestAnswer> prt = _appDbContext.Set<PreTestAnswer>()
                            .Include(x => x.Option)
                            .ThenInclude(x => x.Question)
                            .ThenInclude(x => x.Module)
                            .AsNoTracking().ToList();

            if (entities != null)
                foreach (var entity in entities)
                {
                    ListSubmittedPRTResponse model = new();
                    model.MapToModel(entity);
                    model.IsSubmitted = prt.Any(x => x.Option.Question.Module.Id == entity.Id && x.IdStudent == idStudent);
                    models.Add(model);
                }
            return models;
        }
        public List<ListSubmittedJResponse> GetListSubmittedJ(Guid idStudent)
        {
            List<ListSubmittedJResponse> models = new();
            List<Module> entities = _appDbContext.Set<Module>().Include(x => x.JAnswers).AsNoTracking().OrderBy(x => x.SeelabsId).ToList();

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
        public List<ListSubmittedFTResponse> GetListSubmittedFT(Guid idStudent)
        {
            List<ListSubmittedFTResponse> models = new();
            List<Module> entities = _appDbContext.Set<Module>().Include(x => x.JAnswers).AsNoTracking().OrderBy(x => x.SeelabsId).ToList();

            if (entities != null)
                foreach (var entity in entities)
                {
                    ListSubmittedFTResponse model = new();
                    model.MapToModel(entity);
                    model.IsSubmitted = false;
                    models.Add(model);
                }
            return models;
        }
        public async Task<List<DetailModuleResponse>> SetAssignmentStatus(BaseModel model)
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
            else if (model is SetJStatusRequest j)
            {
                Module entity = await module.FindAsync(j.Id);
                entity.IsJOpen = j.IsOpen;
            }
            else if (model is SetFTStatusRequest ft)
            {
                Module entity = await module.FindAsync(ft.Id);
                entity.IsFTOpen = ft.IsOpen;
            }
            await _appDbContext.SaveChangesAsync();
            return base.GetAll<DetailModuleResponse>().OrderBy(x => x.SeelabsId).ToList();
        }
        public List<GetSubmissionsResponse> GetSubmissions(GetSubmissionsRequest request)
        {
            List<GetSubmissionsResponse> models = new();
            Module module = _appDbContext.Set<Module>().AsNoTracking().FirstOrDefault(x => x.SeelabsId == request.SeelabsId);
            List<Student> students = _appDbContext.Set<Student>()
                                                  .Include(x => x.User)
                                                  .Include(x => x.PAAnswers).ThenInclude(x => x.Module)
                                                  .Include(x => x.JAnswers).ThenInclude(x => x.Module)
                                                  .Include(x => x.PRTAnswers).ThenInclude(x => x.Option).ThenInclude(x => x.Question).ThenInclude(x => x.Module)
                                                  .Where(x => x.Group == request.Group)
                                                  .OrderBy(x => x.User.Name).AsNoTracking().ToList();

            foreach (var student in students)
            {
                GetSubmissionsResponse model = new();
                List<PreTestAnswer> prtAnswers = student.PRTAnswers.Where(x => x.GetModule().SeelabsId == request.SeelabsId).ToList();
                model.MapToModel(student);
                model.MapToModel(student.User);
                model.MapToModel(module);
                model.MapToModel(student.PAAnswers.FirstOrDefault(x => x.Module.SeelabsId == request.SeelabsId));
                model.MapToModel(student.JAnswers.FirstOrDefault(x => x.Module.SeelabsId == request.SeelabsId));
                model.PRTScore = prtAnswers.Where(x => x.Option.IsTrue).Count() * 10;
                model.PRTDetail = prtAnswers.Select(x => new PreTestAnswerDetail(x)).ToList();
                models.Add(model);
            }
            return models;
        }
        public async Task DeleteAllModules()
        {
            List<Module> modules = _appDbContext.Set<Module>().AsNoTracking().ToList();
            _appDbContext.Set<Module>().RemoveRange(modules);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
