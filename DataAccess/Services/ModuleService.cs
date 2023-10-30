using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IModuleService : IBaseService<Module>
    {
        Task<List<ListModuleResponse>> SetAssignmentStatus(BaseModel model);
        List<GetSubmissionsResponse> GetSubmissions(GetSubmissionsRequest model);
        List<ListSubmittedPAResponse> GetListSubmittedPA(Guid idStudent);
        List<ListSubmittedPRTResponse> GetListSubmittedPRT(Guid idStudent);
        List<ListSubmittedJResponse> GetListSubmittedJ(Guid idStudent);
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
        public async Task<List<ListModuleResponse>> SetAssignmentStatus(BaseModel model)
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
            await _appDbContext.SaveChangesAsync();
            return base.GetAll<ListModuleResponse>().OrderBy(x => x.SeelabsId).ToList();
        }
        public List<GetSubmissionsResponse> GetSubmissions(GetSubmissionsRequest request)
        {
            List<GetSubmissionsResponse> models = new();
            List<Student> students = _appDbContext.Set<Student>().Include(x => x.User).Where(x => x.Group == request.Group).OrderBy(x => x.User.Name).AsNoTracking().ToList();
            List<PreTestAnswer> preTestAnswers = _appDbContext.Set<PreTestAnswer>()
                            .Include(x => x.Option)
                            .ThenInclude(x => x.Question)
                            .ThenInclude(x => x.Module)
                            .Where(x => x.Option.Question.Module.SeelabsId == request.SeelabsId)
                            .AsNoTracking().ToList();
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
                model.PRTScore = preTestAnswers.Where(s => s.IdStudent == student.Id && s.Option.IsTrue).Count() * 10;
                model.PRTDetail = preTestAnswers.Where(s => s.IdStudent == student.Id)
                                                .Select(x => new PreTestAnswerDetail
                                                {
                                                    IdOption = x.IdOption,
                                                    Question = x.Option.Question.Question,
                                                    Answer = x.Option.Option,
                                                    Verdict = x.Option.IsTrue ? "Correct" : "Incorrect"
                                                }).ToList();
                models.Add(model);
            }
            return models;
        }
    }
}
