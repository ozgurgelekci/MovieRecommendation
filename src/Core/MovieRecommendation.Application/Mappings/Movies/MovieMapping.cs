using AutoMapper;
using MovieRecommendation.Application.Features.Queries.Movies.GetByIdMovie;
using MovieRecommendation.Domain.Entities;
using ProductExample.Application.Features.Queries.GetAllProduct;

namespace MovieRecommendation.Application.Mappings.Movies
{
    public class MovieMapping : Profile
    {
        public MovieMapping()
        {
            // By Id
            CreateMap<Movie, GetByIdMovieQueryResponse>().ReverseMap();

            // All
            CreateMap<Movie, GetAllMovieQueryResponse>().ReverseMap();
        }
    }
}
