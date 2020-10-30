using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static VotingSystem.Database.Tests.AppDbContextTests;

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
