using Microsoft.AspNetCore.Identity;

namespace ParkIstra.Models.Main;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        Claims = new HashSet<ApplicationUserClaim>();
        Logins = new HashSet<ApplicationUserLogin>();
        Tokens = new HashSet<ApplicationUserToken>();
        UserRoles = new HashSet<ApplicationUserRole>();
    }

    public virtual UserInfo UserInfo { get; set; }

    public ICollection<ApplicationUserClaim> Claims { get; set; }
    public ICollection<ApplicationUserLogin> Logins { get; set; }
    public ICollection<ApplicationUserToken> Tokens { get; set; }
    public ICollection<ApplicationUserRole> UserRoles { get; set; }
}
