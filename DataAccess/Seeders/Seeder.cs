using Microsoft.AspNetCore.Http;
using SealabAPI.Base;
using SealabAPI.DataAccess.Entities;
using SealabAPI.Helpers;
using SealabAPI.DataAccess.Models;

namespace SealabAPI.DataAccess.Seeders
{
    public static class Seeder
    {
        public static async Task Seed<TEntity, TSeed>(AppDbContext _appDbContext) where TEntity : BaseEntity where TSeed : IBaseSeed<TEntity>, new()
        {
            if (!_appDbContext.Set<TEntity>().Any())
            {
                var entity = new TSeed().GetSeeder();
                await _appDbContext.Set<TEntity>().AddRangeAsync(entity);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
