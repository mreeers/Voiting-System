using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VoitingSystem;
using VotingSystem.Models;

namespace VotingSystem.UI.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IVotingPollFactory _pollFactory;

        public HomeController(IVotingPollFactory pollFactory)
        {
            _pollFactory = pollFactory;
        }

        [HttpPost]
        public VotingPoll Create(VotingPollFactory.Request request)
        {
            return _pollFactory.Create(request);
        }
    }
}
