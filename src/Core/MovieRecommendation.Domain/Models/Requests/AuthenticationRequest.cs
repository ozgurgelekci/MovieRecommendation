namespace MovieRecommendation.Domain.Models.Requests
{
    public class AuthenticationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
