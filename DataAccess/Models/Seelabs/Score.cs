using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Extensions;

namespace SealabAPI.DataAccess.Models
{
    public class Score
    {
        public string Uid { get; set; }
        public bool Status { get; set; }
        public int TP { get; set; }
        public int TA { get; set; }
        public int I1 { get; set; }
        public int I2 { get; set; }
        public int D { get; set; }
    }
    public class ScoreResult {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int TP { get; set; }
        public int TA { get; set; }
        public int I1 { get; set; }
        public int I2 { get; set; }
        public int D1 { get; set; }
        public int D2 { get; set; }
        public int D3 { get; set; }
        public int D4 { get; set; }
    }
    public class ScoreDetail : Score
    {
        public string Name { get; set; }
    }
}
