using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class DetailGroupRequest : ListGroupRequest
    {
        public int Group { get; set; }
    }
}
