using Microsoft.AspNetCore.Identity;

namespace ParkIstra.Models.Main;

public class ApplicationUserLogin : IdentityUserLogin<string>
{
    public ApplicationUser User { get; set; } = default!;
}
