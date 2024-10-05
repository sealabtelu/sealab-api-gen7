using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsListGroupRequest : BaseModel
    {
        public bool search { get; set; } = true;
        public int hari_id { get; set; }
        public int shift_id { get; set; }
        public SeelabsListGroupRequest(ListGroupRequest request)
        {
            hari_id = request.Day;
            shift_id = request.Shift;
        }
    }
}
