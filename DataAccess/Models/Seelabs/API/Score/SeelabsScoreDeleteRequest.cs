using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsScoreDeleteRequest : SeelabsScoreResultRequest
    {
        public SeelabsScoreDeleteRequest(ScoreResultRequest request) : base(request)
        {
            pilihan = 3;
            kelompok_id = request.Group;
            modul = request.Module;
        }
    }
}
