using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class ScoreListRequest : BaseModel
    {
        public int Module { get; set; }
    }
}
