using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Services
{
    public interface IPreliminaryAssignmentAnswerService : IBaseService<PreliminaryAssignmentAnswer>
    {

    }
    public class PreliminaryAssignmentAnswerService : BaseService<PreliminaryAssignmentAnswer>, IPreliminaryAssignmentAnswerService
    {
        public PreliminaryAssignmentAnswerService(AppDbContext appDbContext) : base(appDbContext) { }
        public override async Task<PreliminaryAssignmentAnswer> Create<TModel>(TModel model)
        {
            PreliminaryAssignmentAnswer entity = model.MapToEntity<PreliminaryAssignmentAnswer>();
            if (entity.File != null)
            {
                CreatePreliminaryAssignmentAnswerRequest answerModel = model as CreatePreliminaryAssignmentAnswerRequest;
                Module module = await _appDbContext.Set<Module>().Where(module => module.Id == answerModel.IdModule).AsNoTracking().FirstOrDefaultAsync();
                Student student = await _appDbContext.Set<Student>().Include("User").Where(student => student.Id == answerModel.IdStudent).AsNoTracking().FirstOrDefaultAsync();
                entity.FilePath = $"TP{module.SeelabsId}_{student.Day}_{student.Shift}_{student.Group}_{student.User.Nim}_{student.User.Name}";
                entity.SubmitTime = DateTime.Now;
            }

            await _appDbContext.Set<PreliminaryAssignmentAnswer>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            if (entity.File != null) await FileHelper.UploadFileAsync(entity.GetFileInfo());
            return entity;
        }
    }
}
