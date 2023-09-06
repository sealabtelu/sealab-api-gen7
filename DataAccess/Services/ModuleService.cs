using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IModuleService : IBaseService<Module>
    {

    }
    public class ModuleService : BaseService<Module>, IModuleService
    {
        public ModuleService(AppDbContext appDbContext) : base(appDbContext) { }

    }
}
