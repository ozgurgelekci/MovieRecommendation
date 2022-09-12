using MovieRecommendation.Domain.Entities;
using Newtonsoft.Json;

namespace MovieRecommendation.Persistence.Models
{
    public class ResultMovie
    {
        [JsonProperty("results")]
        public List<Movie> Movies { get; set; }
    }
}
