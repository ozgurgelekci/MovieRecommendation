using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieRecommendation.Domain.Entities;

namespace MovieRecommendation.Persistence.ModelBuilders
{
    public class MovieVoteEntityTypeConfiguration : IEntityTypeConfiguration<MovieVote>
    {
        public void Configure(EntityTypeBuilder<MovieVote> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.Comment)
                .HasMaxLength(500);

            builder.HasOne(r => r.User)
                     .WithMany(r => r.MovieVotes)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired();

            builder.HasOne(r => r.Movie)
                    .WithMany(r => r.MovieVotes)
                   .HasForeignKey(u => u.MovieId)
                   .IsRequired();

            builder.ToTable("MovieVotes");
        }
    }
}
