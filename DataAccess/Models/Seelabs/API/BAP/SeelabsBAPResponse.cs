using AngleSharp.Dom;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsBAPResponse : BaseModel
    {
        public string Date { get; set; }
        public string Shift { get; set; }
        public string Module { get; set; }
        public SeelabsBAPResponse(IElement td)
        {
            Date = td.Children[1].TextContent;
            Shift = td.Children[2].TextContent;
            Module = td.Children[3].TextContent;
        }
    }
}
