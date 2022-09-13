using AutoMapper;
using Hangfire;
using MovieRecommendation.Application.Interfaces.Caching;
using MovieRecommendation.Application.Interfaces.Repositories;
using MovieRecommendation.Persistence.Utilities;

namespace MovieRecommendation.WebAPI.SchedulerServices.Jobs
{
    public class MoviesControlJob
    {
        readonly IMovieRepository _movieRepository;
        readonly ICacheManager _cacheManager;
        readonly IMapper _mapper;

        public MoviesControlJob(IServiceProvider serviceProvider)
        {
            _movieRepository = serviceProvider.GetRequiredService<IMovieRepository>();
            _cacheManager = serviceProvider.GetRequiredService<ICacheManager>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();

            RecurringJob.AddOrUpdate(() => Process(), Cron.Hourly(), TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
        }

        public async Task Process()
        {
            await GetMovies.Invoke(_movieRepository, _cacheManager,_mapper);
        }
    }
}
