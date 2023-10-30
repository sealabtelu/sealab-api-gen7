using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class PreTestAnswerDetail : BaseModel
    {
        public Guid IdOption { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Verdict { get; set; }
    }
}
