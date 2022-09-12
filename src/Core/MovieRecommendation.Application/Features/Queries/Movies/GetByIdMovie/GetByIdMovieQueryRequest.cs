using MediatR;

namespace MovieRecommendation.Application.Features.Queries.Movies.GetByIdMovie
{
    public class GetByIdMovieQueryRequest : IRequest<GetByIdMovieQueryResponse>
    {
        public int Id { get; set; }
    }
}
