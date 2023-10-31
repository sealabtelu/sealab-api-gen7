using System.Globalization;
using AngleSharp.Dom;
using AngleSharp.Text;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsScoreResultResponse : BaseModel
    {
        public int Module { get; set; } 
        public int Shift { get; set; }
        public List<ScoreResult> Scores { get; set; }
        public SeelabsScoreResultResponse(IEnumerable<IElement> tr)
        {
            Module = int.Parse(tr.ElementAt(0).Children[2].TextContent);
            Shift = int.Parse(tr.ElementAt(0).Children[12].TextContent);
            Scores = tr.Select(td => new ScoreResult
            {
                Name = td.Children[1].TextContent?.ToTitleCase(),
                TP = int.Parse(td.Children[3].TextContent),
                TA = int.Parse(td.Children[4].TextContent),
                D1 = int.Parse(td.Children[5].TextContent),
                D2 = int.Parse(td.Children[6].TextContent),
                D3 = int.Parse(td.Children[7].TextContent),
                D4 = int.Parse(td.Children[8].TextContent),
                I1 = int.Parse(td.Children[9].TextContent),
                I2 = int.Parse(td.Children[10].TextContent),
                Date = DateTime.ParseExact(td.Children[11].TextContent, "dd/MMMM/yyyy", CultureInfo.InvariantCulture)
            }).ToList();
        }
    }
}
