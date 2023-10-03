using AngleSharp.Dom;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsScheduleResponse : BaseModel
    {
        public string Nim { get; set; }
        public string Name { get; set; }
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
