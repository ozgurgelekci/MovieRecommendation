using Hangfire;
using MovieRecommendation.Application.Interfaces.Caching;
using MovieRecommendation.Application.Interfaces.Repositories;
using MovieRecommendation.Persistence.Utilities;

namespace MovieRecommendation.WebAPI.SchedulerServices.Jobs
{
    public class UpdateMoviesVoteJob
    {
        readonly IMovieRepository _movieRepository;
        readonly IMovieVoteRepository _movieVoteRepository;
        readonly ICacheManager _cacheManager;

        public UpdateMoviesVoteJob(IServiceProvider serviceProvider)
        {
            _movieRepository = serviceProvider.GetRequiredService<IMovieRepository>();
            _movieVoteRepository = serviceProvider.GetRequiredService<IMovieVoteRepository>();
            _cacheManager = serviceProvider.GetRequiredService<ICacheManager>();

            RecurringJob.AddOrUpdate(() => Process(), Cron.Hourly());
        }

        [AutomaticRetry(Attempts = 2)]
        public void Process()
        {
            UpdateMoviesVote.Invoke(_movieRepository, _movieVoteRepository, _cacheManager);

        }
    }
}
