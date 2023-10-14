using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IModuleService : IBaseService<Module>
    {
        public Task SetAssignmentStatus(BaseModel model);
    }
    public class ModuleService : BaseService<Module>, IModuleService
    {
        public ModuleService(AppDbContext appDbContext) : base(appDbContext) { }
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
