using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IPreTestQuestionService : IBaseService<PreTestQuestion>
    {

    }
    public class PreTestQuestionService : BaseService<PreTestQuestion>, IPreTestQuestionService
    {
        public PreTestQuestionService(AppDbContext appDbContext) : base(appDbContext) { }

    }
}
