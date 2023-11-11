using Microsoft.AspNetCore.Identity;

namespace ParkIstra.Models.Main;

public class ApplicationUserClaim : IdentityUserClaim<string>
{
    public string Description { get; set; }

    public ApplicationUser User { get; set; } = default!;
}
