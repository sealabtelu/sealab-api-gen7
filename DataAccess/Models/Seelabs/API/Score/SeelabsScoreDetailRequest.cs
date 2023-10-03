using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsScoreDetailRequest : SeelabsScoreResultRequest
    {
        public SeelabsScoreDetailRequest(ScoreResultRequest request) : base(request)
        {
            pilihan = 1;
            kelompok_id = request.Group;
            modul = request.Module;
        }
    }
}
