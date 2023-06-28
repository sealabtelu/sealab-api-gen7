using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IPreliminaryAssignmentAnswerService : IBaseService<PreliminaryAssignmentAnswer>
    {

    }
    public class PreliminaryAssignmentAnswerService : BaseService<PreliminaryAssignmentAnswer>, IPreliminaryAssignmentAnswerService
    {
        public PreliminaryAssignmentAnswerService(AppDbContext appDbContext) : base(appDbContext) { }

    }
}
