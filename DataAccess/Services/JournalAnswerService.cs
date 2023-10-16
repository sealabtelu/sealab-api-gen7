using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Services
{
    public interface IJournalAnswerService : IBaseService<JournalAnswer>
    {

    }
    public class JournalAnswerService : BaseService<JournalAnswer>, IJournalAnswerService
    {
        public JournalAnswerService(AppDbContext appDbContext) : base(appDbContext) { }
        public override async Task<JournalAnswer> Create<TModel>(TModel model)
        {
            JournalAnswer entity = model.MapToEntity<JournalAnswer>();
            if (entity.File != null)
            {
                CreateJournalAnswerRequest answerModel = model as CreateJournalAnswerRequest;
                Module module = await _appDbContext.Set<Module>().Where(module => module.Id == answerModel.IdModule).AsNoTracking().FirstOrDefaultAsync();
                Student student = await _appDbContext.Set<Student>().Include("User").Where(student => student.Id == answerModel.IdStudent).AsNoTracking().FirstOrDefaultAsync();
                entity.FilePath = $"J{module.SeelabsId}_{student.Day}_{student.Shift}_{student.Group}_{student.User.Nim}_{((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds()}_{student.User.Name}";
                entity.SubmitTime = DateTime.Now;
            }

            await _appDbContext.Set<JournalAnswer>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            if (entity.File != null) await FileHelper.UploadFileAsync(entity.GetFileInfo());
            return entity;
        }
    }
}
