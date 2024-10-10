using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Seeders
{
    public class AssistantSeed : IBaseSeed<Assistant>
    {
        private readonly List<Assistant> Assistants = new()
        {
            new(){
                Code="AI",
                Position="Master Admin",
                User = new(){
                    Name = "PIA-chan",
                    Nim = "0000000000",
                    Username = "piachan",
                    Password = "master",
                    Role = "Assistant"
                }
            }
        };
        public List<Assistant> GetSeeder()
        {
            return Assistants;
        }
    }
}
