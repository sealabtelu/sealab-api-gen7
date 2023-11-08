using System.Globalization;
using AngleSharp.Dom;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsProctorResponse : BaseModel
    {
        public string Subject { get; set; }
        public string Room { get; set; }
        public string Day { get; set; }
        public string Shift { get; set; }
        public SeelabsProctorResponse(IElement td)
        {

            Subject = td.Children[1].TextContent.Trim();
            Room = td.Children[2].TextContent;
            Day = td.Children[3].TextContent;
            Shift = td.Children[4].TextContent;
        }
    }
}
