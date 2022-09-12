using MovieRecommendation.Application.Interfaces.Caching;
using MovieRecommendation.Application.Interfaces.Repositories;

namespace MovieRecommendation.Persistence.Utilities
{
    public static class UpdateMoviesVote
    {
        public static void Invoke(IMovieRepository movieRepository, IMovieVoteRepository movieVoteRepository, ICacheManager cacheManager)
        {
            var votes = movieVoteRepository.GetAsync().Result;

            if (votes.Any())
            {
                var listByMovieId = votes.DistinctBy(x => x.MovieId).ToList();

                foreach (var item in listByMovieId)
                {
                    int voteCount = votes.Count(x => x.MovieId == item.MovieId);
                    int voteSum = votes.Where(x => x.MovieId == item.MovieId).Select(x => x.Vote).Sum();

                    int voteAverage = voteSum / voteCount;

                    var movieId = item.MovieId;

                    var movie = movieRepository.GetByIdAsync(movieId).Result;

                    if (movie != null)
                    {
                        movie.VoteCount = voteCount;
                        movie.VoteAverage = voteAverage;

                        var updatedMovie = movieRepository.Update(movie);

                        cacheManager.Set($"{movie.Id}", updatedMovie, 360000);
                    }
                }
            }
        }
    }
}
