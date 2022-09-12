using MovieRecommendation.Domain.Entities;

namespace MovieRecommendation.Application.Interfaces.Repositories
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
       Movie GetByIdWithChildAsync(int id);
    }
}
