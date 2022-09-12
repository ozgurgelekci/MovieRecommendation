using Microsoft.EntityFrameworkCore;
using MovieRecommendation.Application.Interfaces.Repositories;
using MovieRecommendation.Domain.Entities;
using MovieRecommendation.Persistence.Context;
using MovieRecommendation.Persistence.Repositories.Base;

namespace MovieRecommendation.Persistence.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        readonly ApplicationDbContext _applicationDbContext;

        public MovieRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Movie GetByIdWithChildAsync(int id) => _applicationDbContext.Movies.Include(x=>x.MovieVotes).SingleOrDefault(x => x.Id == id);
    }
}
