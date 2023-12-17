using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IGFormSurveyService : IBaseService<GFormSurvey>
    {
        Task<bool> Verify(Guid id);
    }
    public class GFormSurveyService : BaseService<GFormSurvey>, IGFormSurveyService
    {
        public GFormSurveyService(AppDbContext appDbContext) : base(appDbContext) { }
        public async Task<bool> Verify(Guid id)
        {
            return await _appDbContext.Set<GFormSurvey>().FindAsync(id) != null;
        }
    }
}
