using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IPostService : IBaseService<Post>
    {

    }
    public class PostService : BaseService<Post>, IPostService
    {
        public PostService(AppDbContext appDbContext) : base(appDbContext) { }

    }
}
