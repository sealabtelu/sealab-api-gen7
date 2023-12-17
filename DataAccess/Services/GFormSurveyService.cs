using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Services
{
    public interface IGFormSurveyService : IBaseService<GFormSurvey>
    {

    }
    public class GFormSurveyService : BaseService<GFormSurvey>, IGFormSurveyService
    {
        public GFormSurveyService(AppDbContext appDbContext) : base(appDbContext) { }

    }
}
