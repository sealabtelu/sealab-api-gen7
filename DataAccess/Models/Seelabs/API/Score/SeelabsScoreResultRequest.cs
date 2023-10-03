using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsScoreResultRequest : BaseModel
    {
        public int pilihan { get; set; } = 2;
        public int kelompok_id { get; set; }
        public int modul { get; set; }
        public SeelabsScoreResultRequest(ScoreResultRequest request)
        {
            kelompok_id = request.Group;
            modul = request.Module;
        }
    }
}
