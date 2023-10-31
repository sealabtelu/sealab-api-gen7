using System.Globalization;
using AngleSharp.Dom;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsBAPResponse : BaseModel
    {
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string Module { get; set; }
        public SeelabsBAPResponse(IElement td)
        {

            Date = DateTime.ParseExact(td.Children[1].TextContent, "dd/MMMM/yyyy", CultureInfo.InvariantCulture);
            Shift = td.Children[2].TextContent;
            Module = td.Children[3].TextContent;
        }
    }
}
