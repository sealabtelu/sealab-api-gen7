using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class DeleteStudentRequest : BaseModel
    {
        public Guid Id { get; set; }
    }
}
