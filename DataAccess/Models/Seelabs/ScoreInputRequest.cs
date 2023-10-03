using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Extensions;

namespace SealabAPI.DataAccess.Models
{
    public class ScoreInputRequest : DetailGroupRequest
    {
        public int Module { get; set; }
        public DateTime Date { get; set; }
        public List<Score> Scores { get; set; }
        private List<string> _uid => Scores.Select(s => s.Uid).ToList();
        private List<string> _status => Scores.Select(s => s.Status ? "1" : "0").ToList();
        private List<string> _tp => Scores.Select(s => s.TP.ToString()).ToList();
        private List<string> _ta => Scores.Select(s => s.TA.ToString()).ToList();
        private List<string> _d => Scores.Select(s => s.D.ToString()).ToList();
        private List<string> _i1 => Scores.Select(s => s.I1.ToString()).ToList();
        private List<string> _i2 => Scores.Select(s => s.I2.ToString()).ToList();

        public void GetScores(List<KeyValuePair<string, string>> request)
        {
            var score = new Dictionary<string, dynamic>{
                        {"uid[]", _uid},
                        {"status[]", _status},
                        {"TP[]", _tp},
                        {"TA[]", _ta},
                        {"D1[]", _d},
                        {"D2[]", _d},
                        {"D3[]", _d},
                        {"D4[]", _d},
                        {"I1[]", _i1},
                        {"I2[]", _i2}
                    };

            foreach (var item in score)
                foreach (string arr in item.Value)
                    request.AddKey(item.Key, arr);
        }
    }
}
