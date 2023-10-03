using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsScoreListRequest : BaseModel
    {
        public bool search { get; set; } = true;
        public int modul_id { get; set; }
        public SeelabsScoreListRequest(ScoreListRequest request)
        {
            modul_id = request.Module;
        }
    }
}
