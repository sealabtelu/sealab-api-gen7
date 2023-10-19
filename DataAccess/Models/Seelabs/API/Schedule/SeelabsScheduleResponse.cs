using AngleSharp.Dom;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsScheduleResponse : BaseModel
    {
        private string _name;
        public string Nim { get; set; }
        public string Name { get => _name; set => _name = value.ToTitleCase(); }
        public string Day { get; set; }
        public string Shift { get; set; }
        public SeelabsScheduleResponse(IElement td)
        {
            Nim = td.Children[1].TextContent;
            Name = td.Children[2].TextContent;
            Day = td.Children[3].TextContent;
            Shift = td.Children[4].TextContent;
        }
    }
}
