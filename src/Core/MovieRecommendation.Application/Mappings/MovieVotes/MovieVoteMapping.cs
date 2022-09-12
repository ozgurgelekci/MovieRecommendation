using AutoMapper;
using MovieRecommendation.Application.Features.Commands.MovieVotes;
using MovieRecommendation.Application.Features.Queries.Movies.GetByIdMovie;
using MovieRecommendation.Domain.Entities;

namespace MovieRecommendation.Application.Mappings.MovieVotes
{
    public class MovieVoteMapping : Profile
    {
        public MovieVoteMapping()
        {
            CreateMap<MovieVote, CreateMovieVoteCommandRequest>().ReverseMap();

            CreateMap<MovieVote, GetByIdMovieVoteQueryResponse>().ReverseMap();
        }
    }
}
