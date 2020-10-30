using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VoitingSystem.Application;
using VoitingSystem.Models;
using VotingSystem.Database.Tests.Infrastructure;
using Xunit;
using static VotingSystem.Database.Tests.AppDbContextTests;

namespace VotingSystem.Database.Tests
{
    public class VotingSystemPersistanceTests
    {
        [Fact]
        public void PersistsVotingPoll()
        {
            var poll = new VotingPoll
            {
                Title = "title",
                Description = "desc",
                Counters = new List<Counter> 
                {
                    new Counter { Name = "One"},
                    new Counter { Name = "Two"}
                }
            };

            using (var ctx = DbContextFactory.Create(nameof(PersistsVotingPoll)))
            {
                IVotingSystemPersistance persistance = new VotingSystemPersistance(ctx);
                persistance.SaveVotingPoll(poll);
            }

            using (var ctx = DbContextFactory.Create(nameof(PersistsVotingPoll)))
            {
                var savePoll = ctx.VotingPolls
                    .Include(x => x.Counters)
                    .Single();

                Assert.Equal(poll.Title, savePoll.Title);
                Assert.Equal(poll.Description, savePoll.Description);
                Assert.Equal(poll.Counters.Count(), savePoll.Counters.Count());

                foreach(var name in poll.Counters.Select(x => x.Name))
                {
                    Assert.Contains(name, savePoll.Counters.Select(x => x.Name));
                }
            }
        }
        
    }

    public class VotingSystemPersistance : IVotingSystemPersistance
    {
        private readonly AppDbContext _ctx;

        public VotingSystemPersistance(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void SaveVotingPoll(VotingPoll votingPoll)
        {
            _ctx.VotingPolls.Add(votingPoll);
            _ctx.SaveChanges();
        }
    }
}
