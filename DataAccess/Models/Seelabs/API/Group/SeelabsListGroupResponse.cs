using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsListGroupResponse : BaseModel
    {
        public int Group { get; set; }
        public string[] Names { get; set; }
        public SeelabsListGroupResponse(dynamic group)
        {
            Group = group.id_group;
            Names = ((List<string>)group.names).Select(name => name[2..]).ToArray();
        }
    }
}
