﻿using System;
using System.Linq;
using System.Text;
using VoitingSystem.Models;
using Xunit;

namespace VoitingSystem.Tests
{
    public class VotingPollTest
    {
        [Fact]
        public void ZeroCountersWhenCreated()
        {
            var poll = new VotingPoll();
            Assert.Empty(poll.Counters);
        }
    }

    public class VotingPollFactoryTests
    {
        private VotingPollFactory _factory = new VotingPollFactory();
        private VotingPollFactory.Request _request = new VotingPollFactory.Request
        {
            Title = "title",
            Description = "description",
            Names = new[] { "name1", "name2" }
        };
        

        
        [Fact]
        public void Create_ThrowWhenIfLessThanTwoCounterNames()
        {
            _request.Names = new[] { "name" };
            Assert.Throws<ArgumentException>(() => _factory.Create(_request));
            _request.Names = new string[] { };
            Assert.Throws<ArgumentException>(() => _factory.Create(_request));
        }

        [Fact]
        public void Create_AddsCounterToThePollForEachProvidedName()
        {
            var poll = _factory.Create(_request);

            foreach(var name in _request.Names)
            {
                Assert.Contains(name, poll.Counters.Select(x => x.Name));
            }
        }

        [Fact]
        public void Create_AddsTitleToThePoll()
        {
            var poll = _factory.Create(_request);
            Assert.Equal(_request.Title, poll.Title);
        }

        [Fact]
        public void Create_AddsDescriptionToThePoll()
        {
            var poll = _factory.Create(_request);
            Assert.Equal(_request.Description, poll.Description);
        }
    }
}
