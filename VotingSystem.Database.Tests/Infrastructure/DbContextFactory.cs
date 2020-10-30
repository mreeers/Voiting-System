using Microsoft.EntityFrameworkCore;
using VotingSystem.Database;

namespace VotingSystem.Database.Tests.Infrastructure
{
    public class DbContextFactory
    {
        public static AppDbContext Create(string databaseName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            return new AppDbContext(options);
        }
    }
}
