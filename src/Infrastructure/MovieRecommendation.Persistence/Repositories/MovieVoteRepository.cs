using MovieRecommendation.Application.Interfaces.Repositories;
using MovieRecommendation.Domain.Entities;
using MovieRecommendation.Persistence.Context;
using MovieRecommendation.Persistence.Repositories.Base;

namespace MovieRecommendation.Persistence.Repositories
{
    public class MovieVoteRepository : BaseRepository<MovieVote>, IMovieVoteRepository
    {
        public MovieVoteRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
