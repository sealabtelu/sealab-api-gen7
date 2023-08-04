using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IPreTestOptionService : IBaseService<PreTestOption>
    {

    }
    public class PreTestOptionService : BaseService<PreTestOption>, IPreTestOptionService
    {
        public PreTestOptionService(AppDbContext appDbContext) : base(appDbContext) { }

    }
}
