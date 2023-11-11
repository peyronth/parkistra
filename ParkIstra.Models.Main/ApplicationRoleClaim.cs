using Microsoft.AspNetCore.Identity;

namespace ParkIstra.Models.Main;

public class ApplicationRoleClaim : IdentityRoleClaim<string>
{
    public string Description { get; set; }

    public ApplicationRole Role { get; set; } = default!;
}
