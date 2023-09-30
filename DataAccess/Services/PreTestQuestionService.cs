using Microsoft.EntityFrameworkCore;
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

        public override async Task<PreTestQuestion> Update<TModel>(TModel model)
        {
            PreTestQuestion question = model.MapToEntity<PreTestQuestion>();
            foreach (var item in question.PTOptions)
                if (item.Id == default)
                {
                    await _appDbContext.Set<PreTestOption>().AddAsync(item);
                    question.PTOptions.Remove(item);
                }
            List<PreTestOption> optionsToRemove = _appDbContext.Set<PreTestOption>().Where(o => o.IdQuestion == question.Id).AsNoTracking().ToList();
            optionsToRemove.RemoveAll(existOption => question.PTOptions.Any(newOption => newOption.Id == existOption.Id));
            _appDbContext.Set<PreTestOption>().RemoveRange(optionsToRemove);
            return await base.Update(model);
        }
    }
}
