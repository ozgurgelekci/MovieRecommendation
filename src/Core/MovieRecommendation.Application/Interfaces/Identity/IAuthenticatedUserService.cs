namespace MovieRecommendation.Application.Interfaces.Identity
{
    public interface IAuthenticatedUserService
    {
        string Id { get; }
        string UserEmail { get; }
    }
}
