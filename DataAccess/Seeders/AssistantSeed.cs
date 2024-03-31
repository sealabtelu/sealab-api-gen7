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
                Code="SEA",
                Position="Master Admin",
                User = new(){
                    Name = "SEA-chan",
                    Nim = "0000000000",
                    Username = "seachan",
                    Password = "seantuy",
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
