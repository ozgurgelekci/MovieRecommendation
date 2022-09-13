using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendation.WebAPI.SchedulerServices.Jobs;

namespace MovieRecommendation.WebAPI.SchedulerServices
{
    [Route("[controller]")]
    [ApiController]
    public class InitController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        public InitController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet("Stop")]
        //[Route("/api/Clear")]
        public ActionResult<string> Clear()
        {

            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }


            return Ok("Jobs Stoped");
        }

        [HttpGet("Start")]
        public ActionResult<string> Get()
        {
            new MoviesControlJob(_serviceProvider);
            new UpdateMoviesVoteJob(_serviceProvider);

            return Ok("Jobs started");
        }
    }
}

