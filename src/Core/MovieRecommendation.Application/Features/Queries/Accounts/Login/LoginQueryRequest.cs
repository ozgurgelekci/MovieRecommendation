using MediatR;

namespace MovieRecommendation.Application.Features.Queries.Accounts.Login
{
    public class LoginQueryRequest : IRequest<LoginQueryResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
