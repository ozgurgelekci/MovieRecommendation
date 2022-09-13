using MovieRecommendation.Domain.Entities;
using Newtonsoft.Json;

namespace MovieRecommendation.Application.Features.Queries.Movies.GetByIdMovie
{
    public class GetByIdMovieQueryResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }
     
        public int VoteAverage { get; set; }
       
        public int VoteCount { get; set; }
       
        public List<GetByIdMovieVoteQueryResponse> MovieVotes { get; set; } = new List<GetByIdMovieVoteQueryResponse>();
    }

    public class GetByIdMovieVoteQueryResponse
    {
        public int Vote { get; set; }
      
        public string? Comment { get; set; }
    }
}
