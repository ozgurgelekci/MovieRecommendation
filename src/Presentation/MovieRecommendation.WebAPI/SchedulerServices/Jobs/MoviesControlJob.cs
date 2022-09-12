﻿using Hangfire;
using MovieRecommendation.Application.Interfaces.Caching;
using MovieRecommendation.Application.Interfaces.Repositories;
using MovieRecommendation.Persistence.Utilities;

namespace MovieRecommendation.WebAPI.SchedulerServices.Jobs
{
    public class MoviesControlJob
    {
        readonly IMovieRepository _movieRepository;
        readonly ICacheManager _cacheManager;

        public MoviesControlJob(IServiceProvider serviceProvider)
        {
            _movieRepository = serviceProvider.GetRequiredService<IMovieRepository>();
            _cacheManager = serviceProvider.GetRequiredService<ICacheManager>();

            RecurringJob.AddOrUpdate(() => Process(), Cron.Hourly());
        }

        [AutomaticRetry(Attempts = 2)]
        public async Task Process()
        {
            await GetMovies.Invoke(_movieRepository, _cacheManager);
        }
    }
}
