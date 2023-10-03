using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsDetailGroupRequest : SeelabsListGroupRequest
    {
        public int aksi { get; set; } = 1;
        public int kelompok_id { get; set; }
        public SeelabsDetailGroupRequest(DetailGroupRequest request) : base(request)
        {
            kelompok_id = request.Group;
        }
    }
}
