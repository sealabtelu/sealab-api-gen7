using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class UpdateJournalAnswerRequest : BaseModel
    {
        public Guid Id { get; set; }
    }
}
