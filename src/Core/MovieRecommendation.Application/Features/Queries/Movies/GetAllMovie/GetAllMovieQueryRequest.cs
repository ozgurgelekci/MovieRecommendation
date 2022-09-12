using MediatR;

namespace ProductExample.Application.Features.Queries.GetAllProduct
{
    public class GetAllMovieQueryRequest : IRequest<List<GetAllMovieQueryResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
