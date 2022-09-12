using MovieRecommendation.Application.Interfaces.Identity;
using System.Security.Claims;

namespace MovieRecommendation.WebAPI.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            Id = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            UserEmail = httpContextAccessor.HttpContext?.User?.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
        }

        public string UserEmail { get; }

        public string Id { get; }
    }
}
