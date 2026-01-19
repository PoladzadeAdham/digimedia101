using Microsoft.AspNetCore.Identity;

namespace digimedia101.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; } = string.Empty;
    }
}
