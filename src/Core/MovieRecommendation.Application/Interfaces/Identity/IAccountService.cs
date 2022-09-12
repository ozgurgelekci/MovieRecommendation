using MovieRecommendation.Application.Features.Queries.Accounts.Login;

namespace MovieRecommendation.Application.Interfaces.Identity
{
    public interface IAccountService
    {
        Task<LoginQueryResponse> Login(LoginQueryRequest request);
        Task<List<LoginQueryResponse>> GetUsers();
    }
}
