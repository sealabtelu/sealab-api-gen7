using AngleSharp.Dom;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsScoreStudentResponse : BaseModel
    {
        public int Module { get; set; }
        public bool Presence { get; set; }
        public int TP { get; set; }
        public int TA { get; set; }
        public int D1 { get; set; }
        public int D2 { get; set; }
        public int D3 { get; set; }
        public int D4 { get; set; }
        public int I1 { get; set; }
        public int I2 { get; set; }
        public string Mentor { get; set; }
        public SeelabsScoreStudentResponse(IElement td)
        {
            Module = int.Parse(td.Children[1].TextContent);
            Presence = td.Children[2].TextContent == "Hadir";
            TP = int.Parse(td.Children[3].TextContent);
            TA = int.Parse(td.Children[4].TextContent);
            D1 = int.Parse(td.Children[5].TextContent);
            D2 = int.Parse(td.Children[6].TextContent);
            D3 = int.Parse(td.Children[7].TextContent);
            D4 = int.Parse(td.Children[8].TextContent);
            I1 = int.Parse(td.Children[9].TextContent);
            I2 = int.Parse(td.Children[10].TextContent);
            Mentor = td.Children[11].TextContent;
        }
    }
}
