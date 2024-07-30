using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IArticleCategoryService : IBaseService<ArticleCategory>
    {

    }
    public class ArticleCategoryService : BaseService<ArticleCategory>, IArticleCategoryService
    {
        public ArticleCategoryService(AppDbContext appDbContext) : base(appDbContext) { }

    }
}
