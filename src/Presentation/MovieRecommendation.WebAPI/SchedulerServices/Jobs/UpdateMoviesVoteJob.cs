using AutoMapper;
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
        readonly IMapper _mapper;

        public UpdateMoviesVoteJob(IServiceProvider serviceProvider)
        {
            _movieRepository = serviceProvider.GetRequiredService<IMovieRepository>();
            _movieVoteRepository = serviceProvider.GetRequiredService<IMovieVoteRepository>();
            _cacheManager = serviceProvider.GetRequiredService<ICacheManager>();
            _mapper = serviceProvider.GetRequiredService<IMapper>(); ;

            RecurringJob.AddOrUpdate(() => Process(), Cron.Hourly(), TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
        }

        public void Process()
        {
            UpdateMoviesVote.Invoke(_movieRepository, _movieVoteRepository, _cacheManager, _mapper);
        }
    }
}
