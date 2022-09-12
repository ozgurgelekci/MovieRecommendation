using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieRecommendation.Domain.Entities;

namespace MovieRecommendation.Persistence.ModelBuilders
{
    public class MovieEntityTypeConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(p => p.Title)
                .IsRequired();

            //builder.HasMany(x=>x.MovieVotes).WithOne(x=>x.Movie).HasForeignKey(x=> x.MovieId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Movies");
        }
    }
}
