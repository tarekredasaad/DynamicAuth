using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{


    public class ApplicationUser:IdentityUser
    {
        //public string UserName { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
