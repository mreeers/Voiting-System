using VotingSystem.Models;

namespace VoitingSystem.Application
{
    public interface IVotingSystemPersistance
    {
        void SaveVotingPoll(VotingPoll votingPoll);
        void SaveVote(Vote vote);
        bool VoteExists(Vote vote);
    }
}
