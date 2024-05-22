using Microsoft.EntityFrameworkCore;
using PosterrPosts.Domain.Entities;
using PosterrPosts.Infra;
using System;
using System.Threading.Tasks;

namespace PosterrPosts.Test.Helper
{
    internal static class DbContextHelper
    {
        internal static async Task<PosterrPostDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<PosterrPostDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new PosterrPostDbContext(options);
            databaseContext.Database.EnsureCreated();
            if ((await databaseContext.Users.CountAsync()) <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    databaseContext.Users.Add(new User()
                    {
                        Id = i,
                        CreationDate = DateTime.Now.Date,
                        UserName = $"User{i}"
                    });
                    await databaseContext.SaveChangesAsync();

                    for(int j = 1; j <= i; j++)
                    {
                        databaseContext.Posts.Add(new Post()
                        {
                            PostText = $"{j} - Teste User",
                            CreateDate = DateTime.Now.Date,
                            PostType = Domain.Enum.EPostType.POST,
                            UserId = i
                        });
                        await databaseContext.SaveChangesAsync();
                    }
                }
            }

            return databaseContext;
        }
    }
}
