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
                    Nim = "0000000000",
                    Username = "seachan",
                    Password = "seantuy",
                    Name = "SEA-chan",
                }
            }
        };
        public List<Assistant> GetSeeder()
        {
            return Assistants;
        }
    }
}
