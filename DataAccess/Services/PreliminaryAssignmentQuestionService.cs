using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IPreliminaryAssignmentQuestionService : IBaseService<PreliminaryAssignmentQuestion>
    {

    }
    public class PreliminaryAssignmentQuestionService : BaseService<PreliminaryAssignmentQuestion>, IPreliminaryAssignmentQuestionService
    {
        public PreliminaryAssignmentQuestionService(AppDbContext appDbContext) : base(appDbContext) { }

    }
}
