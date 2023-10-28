using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class Feedback : BaseModel
    {
        public string Assistant { get; set; }
        public string Session { get; set; }
        public string Laboratory { get; set; }
        public Feedback(JournalAnswer j)
        {
            Assistant = j.AssistantFeedback;
            Session = j.SessionFeedback;
            Laboratory = j.LaboratoryFeedback;
        }
    }
}
