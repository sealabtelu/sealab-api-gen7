using FluentValidation;
using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Models
{
    public class SetPAStatusRequest : BaseModel
    {
        public Guid Id { get; set; }
        public bool IsOpen { get; set; }
        public override TEntity MapToEntity<TEntity>()
        {
            return new Module
            {
                Id = Id,
                IsPAOpen = IsOpen
            } as TEntity;
        }
    }
}
