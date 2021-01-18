using Microsoft.AspNetCore.Identity;

namespace joloochusite.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? AppUserId { get; set; }
    }
}