using MovieRecommendation.Domain.Common;
using Newtonsoft.Json;

namespace MovieRecommendation.Domain.Entities
{
    public class Movie : BaseEntity
    {
        public override int Id { get => base.Id; set => base.Id = value; }

        public string Title { get; set; }

        public int VoteAverage { get; set; }
     
        public int VoteCount { get; set; }

        public ICollection<MovieVote> MovieVotes { get; set; } = new List<MovieVote>();
    }

}
