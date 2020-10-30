using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VoitingSystem.Application;
using VotingSystem.Database.Tests.Infrastructure;
using VotingSystem.Models;
using Xunit;

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

        [Fact]
        public void PersistsVote()
        {
            var vote = new Vote { UserId = "user", CounterId = 1 };
            using (var ctx = DbContextFactory.Create(nameof(PersistsVote)))
            {
                var persistance = new VotingSystemPersistance(ctx);
                persistance.SaveVote(vote);
            }

            using (var ctx = DbContextFactory.Create(nameof(PersistsVote)))
            {
                var savedVote = ctx.Votes.Single();
                Assert.Equal(vote.UserId, savedVote.UserId);
                Assert.Equal(vote.CounterId, savedVote.CounterId);
            }
        }

        [Fact]
        public void VoteExists_RetirnsFalseWhenNoVote()
        {
            var vote = new Vote { UserId = "user", CounterId = 1 };

            using (var ctx = DbContextFactory.Create(nameof(VoteExists_RetirnsFalseWhenNoVote)))
            {
                var persistance = new VotingSystemPersistance(ctx);
                Assert.False(persistance.VoteExists(vote));
            }
        }

        [Fact]
        public void VoteExists_RetirnsTrueWhenVoteExists()
        {
            var vote = new Vote { UserId = "user", CounterId = 1 };

            using (var ctx = DbContextFactory.Create(nameof(VoteExists_RetirnsTrueWhenVoteExists)))
            {
                ctx.Votes.Add(vote);
                ctx.SaveChanges();
            }

            using (var ctx = DbContextFactory.Create(nameof(VoteExists_RetirnsTrueWhenVoteExists)))
            {
                var persistance = new VotingSystemPersistance(ctx);
                Assert.True(persistance.VoteExists(vote));
            }
        }
    }
}
