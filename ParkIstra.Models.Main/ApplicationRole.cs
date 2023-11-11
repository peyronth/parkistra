using Microsoft.AspNetCore.Identity;

namespace ParkIstra.Models.Main;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole()
    {
        Claims = new HashSet<ApplicationRoleClaim>();
        UserRoles = new HashSet<ApplicationUserRole>();
    }

    public ICollection<ApplicationRoleClaim> Claims { get; set; }
    public ICollection<ApplicationUserRole> UserRoles { get; set; }
}
