using Microsoft.AspNetCore.Identity;

namespace ParkIstra.Models.Main;

public class ApplicationUserRole : IdentityUserRole<string>
{
    public ApplicationUser User { get; set; } = default!;
    public ApplicationRole Role { get; set; } = default!;
}
