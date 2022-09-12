using AutoMapper;
using MediatR;
using MovieRecommendation.Application.Interfaces.Repositories;

namespace ProductExample.Application.Features.Queries.GetAllProduct
{
    public class GetAllMovieQueryHandler : IRequestHandler<GetAllMovieQueryRequest, List<GetAllMovieQueryResponse>>
    {
        readonly IMovieRepository _movieRepository;
        IMapper _mapper;
        public GetAllMovieQueryHandler(IMapper mapper, IMovieRepository movieRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
        }
        public async Task<List<GetAllMovieQueryResponse>> Handle(GetAllMovieQueryRequest request, CancellationToken cancellationToken)
        {
            var movies = _movieRepository.GetPagedList(request.PageNumber, request.PageSize);
            return _mapper.Map<List<GetAllMovieQueryResponse>>(movies);
        }
    }
}
