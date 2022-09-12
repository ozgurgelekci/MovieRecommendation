using Microsoft.AspNetCore.Identity;

namespace MovieRecommendation.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

        public ICollection<MovieVote> MovieVotes { get; set; }
    }
}
