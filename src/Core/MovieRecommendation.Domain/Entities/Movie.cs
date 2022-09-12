using MovieRecommendation.Domain.Common;
using Newtonsoft.Json;

namespace MovieRecommendation.Domain.Entities
{
    public class Movie : BaseEntity
    {
        [JsonProperty("id")]
        public override int Id { get => base.Id; set => base.Id = value; }

        [JsonProperty("original_title")]
        public string Title { get; set; }

        public int VoteAverage { get; set; }
        public int VoteCount { get; set; }


        public ICollection<MovieVote> MovieVotes { get; set; } = new List<MovieVote>();
    }

}
