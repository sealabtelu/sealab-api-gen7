using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsScoreUpdateRequest : SeelabsScoreDetailRequest
    {
        public bool search { get; set; } = true;
        public int modulid => modul;
        public int praktikum_id { get; set; } = 114;
        public string editinput { get; set; } = "submit";
        public SeelabsScoreUpdateRequest(ScoreResultRequest request) : base(request)
        {
        }
    }
}
