using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class ScoreListGroupRequest : BaseModel
    {
        public int Day { get; set; }
        public int Shift { get; set; }
        public int? Group { get; set; }
    }
}
