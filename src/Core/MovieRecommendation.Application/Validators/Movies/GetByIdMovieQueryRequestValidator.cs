using FluentValidation;
using MovieRecommendation.Application.Constants;
using MovieRecommendation.Application.Features.Queries.Movies.GetByIdMovie;

namespace MovieRecommendation.Application.Validators.Movies
{
    public class GetByIdMovieQueryRequestValidator : AbstractValidator<GetByIdMovieQueryRequest>
    {
        public GetByIdMovieQueryRequestValidator()
        {
            RuleFor(request => request.Id)
            .NotNull()
            .WithMessage(ValidationMessage.InvalidId)
            .NotEmpty()
            .WithMessage(ValidationMessage.InvalidId)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0");
        }
    }
}
