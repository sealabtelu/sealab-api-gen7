using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class BAPRequest : BaseModel
    {
        public DateTime date { get; set; }
    }
}
