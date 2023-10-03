using AngleSharp.Dom;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsBAPRequest : BaseModel
    {
        public string first_date { get; set; }
        public SeelabsBAPRequest(BAPRequest request)
        {
            first_date = request.Date.ToString("yyyy-MM-dd");
        }
    }
}
