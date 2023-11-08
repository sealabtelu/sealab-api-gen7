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
        public int Day { get; set; }
        public string Shift { get; set; }
        public SeelabsProctorResponse(IElement td)
        {
            Dictionary<string, int> dayMap = new()
            {
                {"Senin", 1},
                {"Selasa", 2},
                {"Rabu", 3},
                {"Kamis", 4},
                {"Jum'at", 5},
                {"Sabtu", 6}
            };
            Subject = td.Children[1].TextContent.Trim();
            Room = td.Children[2].TextContent;
            Day = dayMap[td.Children[3].TextContent.Split(' ')[0]];
            Shift = td.Children[4].TextContent;
        }
    }
}
