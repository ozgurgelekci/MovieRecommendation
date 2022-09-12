namespace MovieRecommendation.Application.Features.Queries.Accounts.Login
{
    public class LoginQueryResponse
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Id { get; set; }
        public string JWToken { get; set; }
        public bool IsVerified { get; set; }
    }
}
