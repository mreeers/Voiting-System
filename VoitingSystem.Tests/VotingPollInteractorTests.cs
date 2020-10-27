﻿using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace VoitingSystem.Tests
{
    public class VotingPollInteractorTests
    {
        private VotingPollFactory.Request _request = new VotingPollFactory.Request();
        private Mock<IVotingPollFactory> _mockFactory = new Mock<IVotingPollFactory>();
        private Mock<IVotingSystemPersistance> _mockPersistance = new Mock<IVotingSystemPersistance>();
        private VotingPollInteractor _interactor;

        public VotingPollInteractorTests()
        {
            _interactor = new VotingPollInteractor(_mockFactory.Object, _mockPersistance.Object);
        }

        [Fact]
        public void CreateVotingPoll_UsesVotingPollFactoryToCreateVotingPoll()
        {
            var factory = new VotingPollFactory();

            _interactor.CreateVotingPoll(_request);

            _mockFactory.Verify(x => x.Create(_request));
        }

        [Fact]
        public void CreateCotingPoll_PersistsCreatedPoll()
        {
            var poll = new VotingPoll();
            _mockFactory.Setup(x => x.Create(_request)).Returns(poll);

            _interactor.CreateVotingPoll(_request);

            _mockPersistance.Verify(x => x.SaveVotingPoll(poll));
        }
    }

    public interface IVotingSystemPersistance
    {
        void SaveVotingPoll(VotingPoll votingPoll);
    }

    public class VotingPollInteractor
    {
        private readonly IVotingPollFactory _factory;
        private readonly IVotingSystemPersistance _persistance;

        public VotingPollInteractor(IVotingPollFactory factory, IVotingSystemPersistance persistance)
        {
            _factory = factory;
            _persistance = persistance;
        }

        public void CreateVotingPoll(VotingPollFactory.Request request)
        {
            var poll = _factory.Create(request);
            _persistance.SaveVotingPoll(poll);
        }
    }
}
