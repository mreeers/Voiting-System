using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VoitingSystem;
using VotingSystem.Models;

namespace VotingSystem.UI.Pages
{
    public class IndexModel : PageModel
    {
        public VotingPoll Poll { get; set; }

        [BindProperty]
        public VotingPollFactory.Request Form { get; set; }

        public void OnGet([FromServices] IVotingPollFactory pollFactory)
        {
            //var request = new VotingPollFactory.Request
            //{
            //    Title = "Title",
            //    Description = "desc",
            //    Names = new[] { "one", "two" }
            //};

            //Poll = pollFactory.Create(request);
        }

        public void OnPost([FromServices] IVotingPollFactory pollFactory)
        {
            Poll = pollFactory.Create(Form);
        }
    }
}
