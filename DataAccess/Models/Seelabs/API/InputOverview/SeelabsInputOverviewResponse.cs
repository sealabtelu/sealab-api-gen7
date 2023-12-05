using System.Globalization;
using AngleSharp.Dom;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

namespace SealabAPI.DataAccess.Models
{
    public class SeelabsInputOverviewResponse : BaseModel
    {
        public string Mentor { get; set; }
        public int Group { get; set; }
        public List<InputOverviewDetail> StudentList { get; set; }
    }
}
