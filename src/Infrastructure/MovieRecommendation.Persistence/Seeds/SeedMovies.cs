using AutoMapper;
using MovieRecommendation.Application.Interfaces.Caching;
using MovieRecommendation.Application.Interfaces.Repositories;
using MovieRecommendation.Persistence.Utilities;

namespace MovieRecommendation.Persistence.Seeds
{
    public static class SeedMovies
    {
        public static async Task SeedAsync(IMovieRepository movieRepository, ICacheManager cacheManager,IMapper mapper)
        {
            await GetMovies.Invoke(movieRepository, cacheManager,mapper);
        }
    }
}
