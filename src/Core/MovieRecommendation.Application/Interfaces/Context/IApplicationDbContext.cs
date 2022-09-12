using Microsoft.EntityFrameworkCore;
using MovieRecommendation.Domain.Entities;

namespace MovieRecommendation.Application.Interfaces.Context
{
    public interface IApplicationDbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieVote> MovieVotes { get; set; }
    }
}
