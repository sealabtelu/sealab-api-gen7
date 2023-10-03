using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class ListGroupRequest : BaseModel
    {
        public int Day { get; set; }
        public int Shift { get; set; }
    }
}
