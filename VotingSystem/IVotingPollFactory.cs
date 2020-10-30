using VoitingSystem.Models;

namespace VoitingSystem
{
    public interface IVotingPollFactory
    {
        VotingPoll Create(VotingPollFactory.Request request);
    }
}
