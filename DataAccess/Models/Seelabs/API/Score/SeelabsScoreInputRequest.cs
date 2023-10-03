using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsScoreInputRequest : SeelabsDetailGroupRequest
    {
        public int modulid { get; set; }
        public int praktikum_id { get; set; } = 114;
        public string tanggal { get; set; }
        public string submit { get; set; } = "submit";
        public string terms { get; set; } = "on";
        public SeelabsScoreInputRequest(ScoreInputRequest request) : base(request)
        {
            modulid = request.Module;
            tanggal = request.Date.ToString("yyyy-MM-dd");
        }
    }
}
