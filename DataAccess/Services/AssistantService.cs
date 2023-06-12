using AutoMapper;
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
        Task<List<Assistant>> BulkInsert(List<CreateAssistantRequest> model);
    }
    public class AssistantService : BaseService<Assistant>, IAssistantService
    {
        public AssistantService(AppDbContext appDbContext) : base(appDbContext) { }
        public async Task<List<Assistant>> BulkInsert(List<CreateAssistantRequest> excel)
        {
            List<Assistant> assistants = new();
            foreach (var row in excel)
            {
                Assistant assistant = row.MapToEntity<Assistant>();
                assistant.User = row.MapToEntity<User>();
                assistant.User.Role = "Assistant";
                assistant.User.Email = assistant.User.Username;
                assistants.Add(assistant);
                await _appDbContext.Set<Assistant>().AddAsync(assistant);
            }

            await _appDbContext.SaveChangesAsync();
            return assistants;
        }
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

