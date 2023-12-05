using System.Globalization;
using AngleSharp.Dom;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class InputOverviewList : InputOverviewDetail
    {
        public string Mentor { get; set; }
        public int Group { get; set; }
        public InputOverviewList(string assistantName, IElement td)
        {
            int span = td.Children[0].GetAttribute("rowspan") != null ? 2 : 0;
            Mentor = assistantName;
            Group = int.Parse(td.Children[0 + span].TextContent);
            Name = td.Children[1 + span].TextContent;
            InputDate = DateTime.ParseExact(td.Children[2 + span].TextContent, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}
