using System.Globalization;
using AngleSharp.Dom;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class InputOverviewDetail
    {
        public string Name { get; set; }
        public DateTime InputDate { get; set; }
        public InputOverviewDetail() { }
        public InputOverviewDetail(InputOverviewList data)
        {
            Name = data.Name;
            InputDate = data.InputDate;
        }
    }
}
