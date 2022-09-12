using MediatR;
using MovieRecommendation.Application.Features.Queries.Accounts.Login;
using MovieRecommendation.Application.Interfaces.Identity;

namespace MovieRecommendation.Application.Features.Commands.Accounts.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQueryRequest, LoginQueryResponse>
    {
        readonly IAccountService _accountService;

        public LoginQueryHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<LoginQueryResponse> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _accountService.Login(request);

            return result;
        }
    }
}
