using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;

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

    public class ScoreInputRequest : ScoreListGroupRequest
    {
        public int Module { get; set; }
        public DateTime Date { get; set; }
        public List<Score> Scores { get; set; }
        public List<string> GetUid()
        {
            return Scores.Select(s => s.Uid).ToList();
        }
        public List<string> GetStatus()
        {
            return Scores.Select(s => s.Status? "1" : "0").ToList();
        }
        public List<string> GetTP()
        {
            return Scores.Select(s => s.TP.ToString()).ToList();
        }
        public List<string> GetTA()
        {
            return Scores.Select(s => s.TA.ToString()).ToList();
        }
        public List<string> GetD()
        {
            return Scores.Select(s => s.D.ToString()).ToList();
        }
        public List<string> GetI1()
        {
            return Scores.Select(s => s.I1.ToString()).ToList();
        }
        public List<string> GetI2()
        {
            return Scores.Select(s => s.I2.ToString()).ToList();
        }

    }
}
