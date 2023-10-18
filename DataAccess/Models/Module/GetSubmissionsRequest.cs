using AutoMapper;
using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class GetSubmissionsRequest : BaseModel
    {
        public int SeelabsId { get; set; }
        public int Group { get; set; }
    }
}
