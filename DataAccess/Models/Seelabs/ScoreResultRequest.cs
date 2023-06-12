using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class ScoreResultRequest : BaseModel
    {
        public int Module { get; set; }
        public int? Group { get; set; }
    }
}
