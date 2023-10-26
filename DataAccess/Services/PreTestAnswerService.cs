using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IPreTestAnswerService : IBaseService<PreTestAnswer>
    {

    }
    public class PreTestAnswerService : BaseService<PreTestAnswer>, IPreTestAnswerService
    {
        public PreTestAnswerService(AppDbContext appDbContext) : base(appDbContext) { }
        public override async Task<PreTestAnswer> Create<TModel>(TModel model)
        {
            List<PreTestAnswer> answers = model.MaptoListEntity<PreTestAnswer>();
            await _appDbContext.Set<PreTestAnswer>().AddRangeAsync(answers);
            await _appDbContext.SaveChangesAsync();
            return answers.FirstOrDefault();
        }
    }
}
