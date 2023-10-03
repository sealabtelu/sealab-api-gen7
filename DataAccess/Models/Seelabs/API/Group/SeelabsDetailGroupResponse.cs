using AngleSharp.Dom;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsDetailGroupResponse : BaseModel
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public SeelabsDetailGroupResponse(IElement td)
        {
            Uid = td.QuerySelector("input").GetAttribute("value");
            Name = td.Children[1].TextContent;
        }
    }
}
