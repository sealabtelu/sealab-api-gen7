using AngleSharp.Dom;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsDetailGroupResponse : BaseModel
    {
        private string _name;
        public string Uid { get; set; }
        public string Name { get => _name; set => _name = value.ToTitleCase(); }
        public SeelabsDetailGroupResponse(IElement td)
        {
            Uid = td.QuerySelector("input").GetAttribute("value");
            Name = td.Children[1].TextContent;
        }
    }
}
