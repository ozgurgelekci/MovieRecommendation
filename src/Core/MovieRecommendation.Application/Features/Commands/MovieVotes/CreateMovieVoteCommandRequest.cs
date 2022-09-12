using MediatR;
using MovieRecommendation.Application.Responses;

namespace MovieRecommendation.Application.Features.Commands.MovieVotes
{
    public class CreateMovieVoteCommandRequest : IRequest<ServiceResponse>
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public int Vote { get; set; }
        public string? Comment { get; set; }
    }
}
