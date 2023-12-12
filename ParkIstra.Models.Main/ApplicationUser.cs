using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ParkIstra.Models.Main;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        Claims = new HashSet<ApplicationUserClaim>();
        Logins = new HashSet<ApplicationUserLogin>();
        Tokens = new HashSet<ApplicationUserToken>();
        UserRoles = new HashSet<ApplicationUserRole>();
        Projects = new HashSet<Project>();
    }

    [Required(ErrorMessage = "This field is required")]
    public int? UserType { get; set; }
    public virtual UserInfo UserInfo { get; set; }

    public ICollection<ApplicationUserClaim> Claims { get; set; }
    public ICollection<ApplicationUserLogin> Logins { get; set; }
    public ICollection<ApplicationUserToken> Tokens { get; set; }
    public ICollection<ApplicationUserRole> UserRoles { get; set; }
    public ICollection<Project> Projects { get; set; }
}
