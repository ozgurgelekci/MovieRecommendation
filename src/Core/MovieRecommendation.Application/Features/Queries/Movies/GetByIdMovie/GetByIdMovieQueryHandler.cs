using AutoMapper;
using MediatR;
using MovieRecommendation.Application.Interfaces.Caching;
using MovieRecommendation.Application.Interfaces.Repositories;
using MovieRecommendation.Domain.Entities;
using Newtonsoft.Json;

namespace MovieRecommendation.Application.Features.Queries.Movies.GetByIdMovie
{
    public class GetByIdMovieQueryHandler : IRequestHandler<GetByIdMovieQueryRequest, GetByIdMovieQueryResponse>
    {
        readonly IMovieRepository _movieRepository;
        readonly ICacheManager _cacheManager;
        IMapper _mapper;
        public GetByIdMovieQueryHandler(IMapper mapper, IMovieRepository movieRepository, ICacheManager cacheManager)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _cacheManager = cacheManager;
        }
        public async Task<GetByIdMovieQueryResponse> Handle(GetByIdMovieQueryRequest request, CancellationToken cancellationToken)
        {

            string movieFromCache = await _cacheManager.GetAsync($"{request.Id}");

            if (movieFromCache != null)
            {
                var deserializeMovie = JsonConvert.DeserializeObject<Movie>(movieFromCache);

                return _mapper.Map<GetByIdMovieQueryResponse>(deserializeMovie);
            }

            Movie movieFromDatabase = _movieRepository.GetByIdWithChildAsync(request.Id);
            return _mapper.Map<GetByIdMovieQueryResponse>(movieFromDatabase);
        }
    }
}
