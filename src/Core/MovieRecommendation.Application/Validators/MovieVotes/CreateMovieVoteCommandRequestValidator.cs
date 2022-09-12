using FluentValidation;
using MovieRecommendation.Application.Constants;
using MovieRecommendation.Application.Features.Commands.MovieVotes;

namespace MovieRecommendation.Application.Validators.MovieVotes
{
    public class CreateMovieVoteCommandRequestValidator : AbstractValidator<CreateMovieVoteCommandRequest>
    {
        public CreateMovieVoteCommandRequestValidator()
        {
            RuleFor(request => request.Vote)
            .NotNull()
            .WithMessage(ValidationMessage.VoteRequired)
            .NotEmpty()
            .WithMessage(ValidationMessage.VoteRequired)
            .GreaterThan(0)
            .WithMessage("Vote must be greater than 0")
            .LessThanOrEqualTo(10)
            .WithMessage("Vote must be less than 10");

            RuleFor(request => request.MovieId)
            .NotNull()
            .WithMessage(ValidationMessage.InvalidMovieId)
            .NotEmpty()
            .WithMessage(ValidationMessage.InvalidMovieId);
        }
    }
}
