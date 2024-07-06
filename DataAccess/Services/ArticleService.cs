using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IArticleService : IBaseService<Article>
    {

    }
    public class ArticleService : BaseService<Article>, IArticleService
    {
        public ArticleService(AppDbContext appDbContext) : base(appDbContext) { }

    }
}
