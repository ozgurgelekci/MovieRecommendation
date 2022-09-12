using Microsoft.AspNetCore.Identity;

namespace MovieRecommendation.Domain.Entities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
