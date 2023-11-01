using AngleSharp.Dom;
using AngleSharp.Text;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsScoreDetailResponse : BaseModel
    {
        public int Module { get; set; }
        public int Group { get; set; }
        public List<ScoreDetail> Scores { get; set; }
        public SeelabsScoreDetailResponse(int module, int group, IEnumerable<IElement> tr)
        {
            Module = module;
            Group = group;
            Scores = tr.Select(td => new ScoreDetail
            {
                Name = td.Children[1].TextContent?.ToTitleCase(),
                Uid = td.QuerySelector("input[name='id[]']")?.GetAttribute("value"),
                Status = td.QuerySelector("option")?.GetAttribute("value") == "1",
                TP = int.Parse(td.QuerySelector("input[name='TP[]']")?.GetAttribute("value")),
                TA = int.Parse(td.QuerySelector("input[name='TA[]']")?.GetAttribute("value")),
                D = int.Parse(td.QuerySelector("input[name='D1[]']")?.GetAttribute("value")),
                I1 = int.Parse(td.QuerySelector("input[name='I1[]']")?.GetAttribute("value")),
                I2 = int.Parse(td.QuerySelector("input[name='I2[]']")?.GetAttribute("value"))
            }).ToList();
        }
    }
}
