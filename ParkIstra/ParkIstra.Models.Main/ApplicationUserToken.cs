using Microsoft.AspNetCore.Identity;

namespace ParkIstra.Models.Main;

public class ApplicationUserToken : IdentityUserToken<string>
{
    public ApplicationUser User { get; set; } = default!;
}
