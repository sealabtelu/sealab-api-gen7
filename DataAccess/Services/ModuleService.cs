using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IModuleService : IBaseService<Module>
    {
        public Task SetAssignmentStatus(BaseModel model);
        List<ListSubmittedPAResponse> GetListSubmittedPA(Guid idStudent);
    }
    public class ModuleService : BaseService<Module>, IModuleService
    {
        public ModuleService(AppDbContext appDbContext) : base(appDbContext) { }
        public List<ListSubmittedPAResponse> GetListSubmittedPA(Guid idStudent)
        {
            List<ListSubmittedPAResponse> models = new();
            List<Module> entities;
            entities = _appDbContext.Set<Module>().Include(x => x.Answers).AsNoTracking().OrderBy(x => x.SeelabsId).ToList();

            if (entities != null)
                foreach (var entity in entities)
                {
                    ListSubmittedPAResponse model = new();
                    model.MapToModel(entity);
                    model.IsSubmitted = entity.Answers.Any(x => x.IdModule == entity.Id && x.IdStudent == idStudent);
                    models.Add(model);
                }
            return models;
        }
        public async Task SetAssignmentStatus(BaseModel model)
        {
            if (model is SetPAStatusRequest pa)
            {
                Module entity = await _appDbContext.Set<Module>().FirstOrDefaultAsync(x => x.Id == pa.Id);
                entity.IsPAOpen = pa.IsOpen;
            }
            else if (model is SetPRTStatusRequest prt)
            {
                Module entity = await _appDbContext.Set<Module>().FirstOrDefaultAsync(x => x.Id == prt.Id);
                entity.IsPRTOpen = prt.IsOpen;
            }
            await _appDbContext.SaveChangesAsync();
        }
    }
}
