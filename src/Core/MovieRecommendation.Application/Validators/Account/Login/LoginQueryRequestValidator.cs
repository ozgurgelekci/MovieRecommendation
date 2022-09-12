using FluentValidation;
using MovieRecommendation.Application.Constants;
using MovieRecommendation.Application.Features.Queries.Accounts.Login;

namespace MovieRecommendation.Application.Validators.Account.Login
{
    public class LoginQueryRequestValidator : AbstractValidator<LoginQueryRequest>
    {
        public LoginQueryRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidationMessage.EmailRequired);

            RuleFor(x => x.Password).NotEmpty().WithMessage(ValidationMessage.PasswordRequired);
        }
    }
}
