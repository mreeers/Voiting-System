using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Database.Tests.Infrastructure;
using VotingSystem.Models;
using Xunit;

namespace VotingSystem.Database.Tests
{
    public class AppDbContextTests
    {
        [Fact]
        public void SaveCounterToDatabase()
        {
            var counter = new Counter { Name = "New Counter"};
            using (var ctx = DbContextFactory.Create(nameof(SaveCounterToDatabase)))
            {
                ctx.Counters.Add(counter);
                ctx.SaveChanges();
            }

            using (var ctx = DbContextFactory.Create(nameof(SaveCounterToDatabase)))
            {
                var saveCounter = ctx.Counters.Single();
                Assert.Equal(counter.Name, saveCounter.Name);
            }
        }

        [Fact]
        public void SaveVotingPollToDatabase()
        {
            var poll = new VotingPoll { Title = "New VotingPoll" };
            using (var ctx = DbContextFactory.Create(nameof(SaveVotingPollToDatabase)))
            {
                ctx.VotingPolls.Add(poll);
                ctx.SaveChanges();
            }

            using (var ctx = DbContextFactory.Create(nameof(SaveVotingPollToDatabase)))

            {
                var savePoll = ctx.VotingPolls.Single();
                Assert.Equal(poll.Title, savePoll.Title);
            }
        }
    }
}
