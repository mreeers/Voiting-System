﻿using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VotingSystem.Models;
using Xunit;

namespace VoitingSystem.Application.Tests
{
    public class VotingInteractorTests
    {
        private Mock<IVotingSystemPersistance> _mockPersistance = new Mock<IVotingSystemPersistance>();
        private readonly VotingInteractor _interactor;
        private readonly Vote _vote = new Vote { UserId = "user", CounterId = 1 };

        public VotingInteractorTests()
        {
            _interactor = new VotingInteractor(_mockPersistance.Object);
        }

        [Fact]
        public void Vote_PersistsVoteWhenUserHasntVoted()
        {
            _interactor.Vote(_vote);

            _mockPersistance.Verify(x => x.SaveVote(_vote));
        }

        [Fact]
        public void Vote_DoesntPersistsVoteWhenUserAlredyVoted()
        {
            _mockPersistance.Setup(x => x.VoteExists(_vote)).Returns(true);

            _interactor.Vote(_vote);

            _mockPersistance.Verify(x => x.SaveVote(_vote), Times.Never);
        }
    }

    public class VotingInteractor
    {
        private readonly IVotingSystemPersistance _persistance;

        public VotingInteractor(IVotingSystemPersistance persistance)
        {
            _persistance = persistance;
        }

        public void Vote(Vote vote)
        {
            if (!_persistance.VoteExists(vote))
            {
                _persistance.SaveVote(vote);
            }
        }
    }
}
