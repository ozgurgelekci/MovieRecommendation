using MediatR;
using MovieRecommendation.Application.Constants;
using MovieRecommendation.Application.Interfaces.Repositories;
using MovieRecommendation.Application.Responses;
using MovieRecommendation.Domain.Entities;

namespace MovieRecommendation.Application.Features.Commands.MovieVotes
{
    public class CreateMovieVoteCommandHandler : IRequestHandler<CreateMovieVoteCommandRequest, ServiceResponse>
    {
        readonly IMovieVoteRepository _movieVoteRepository;

        public CreateMovieVoteCommandHandler(IMovieVoteRepository movieVoteRepository)
        {
            _movieVoteRepository = movieVoteRepository;
        }

        public async Task<ServiceResponse> Handle(CreateMovieVoteCommandRequest request, CancellationToken cancellationToken)
        {
            MovieVote entity = new MovieVote
            {
                Comment = request.Comment,
                MovieId = request.MovieId,
                UserId = request.UserId,
                Vote = request.Vote
            };

            var result = await _movieVoteRepository.AddAsync(entity);

            return result != null
            ? ServiceResponse.CreateSuccess(ResponseMessage.AddedSuccessfully)
            : ServiceResponse.CreateError(ResponseMessage.AddedFailed);
        }
    }
}
