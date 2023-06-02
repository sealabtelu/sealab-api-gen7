using Microsoft.EntityFrameworkCore;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.DataAccess.Models;
using SealabAPI.Helpers;

namespace SealabAPI.DataAccess.Services
{
    public interface IAssistantService : IBaseService<Assistant>
    {
        Task<Assistant> Create(CreateAssistantRequest model);
    }
    public class AssistantService : BaseService<Assistant>, IAssistantService
    {
        public AssistantService(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<Assistant> Create(CreateAssistantRequest model)
        {
            Assistant assistant = model.MapToEntity<Assistant>();
            assistant.User = model.MapToEntity<User>();
            assistant.User.Role = "Assistant";
            assistant.User.Email = assistant.User.Username;

            await _appDbContext.Set<Assistant>().AddAsync(assistant);
            await _appDbContext.SaveChangesAsync();

            return assistant;
        }
    }
}

