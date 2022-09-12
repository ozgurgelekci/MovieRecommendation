using MovieRecommendation.Domain.Common;

namespace MovieRecommendation.Domain.Entities
{
    public class MovieVote : BaseEntity
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public int Vote { get; set; }
        public string? Comment { get; set; }

        public ApplicationUser User { get; set; }
        public Movie Movie { get; set; }

    }
}
