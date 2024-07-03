using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IPostCategoryService : IBaseService<PostCategory>
    {

    }
    public class PostCategoryService : BaseService<PostCategory>, IPostCategoryService
    {
        public PostCategoryService(AppDbContext appDbContext) : base(appDbContext) { }

    }
}
