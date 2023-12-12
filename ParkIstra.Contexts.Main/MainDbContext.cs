using ParkIstra.Contexts.Main.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ParkIstra.Contexts.Main;

public partial class MainDbContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, string,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
{
    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    { }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<UserInfo> UserInfos => Set<UserInfo>();
    public DbSet<Testimonial> Testimonials => Set<Testimonial>();
    public DbSet<Image> Images => Set<Image>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _ = modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        _ = modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
        _ = modelBuilder.ApplyConfiguration(new ApplicationUserClaimConfiguration());
        _ = modelBuilder.ApplyConfiguration(new ApplicationRoleClaimConfiguration());
        _ = modelBuilder.ApplyConfiguration(new ApplicationUserLoginConfiguration());
        _ = modelBuilder.ApplyConfiguration(new ApplicationUserTokenConfiguration());
        _ = modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        _ = modelBuilder.ApplyConfiguration(new UserInfoConfiguration());
        _ = modelBuilder.ApplyConfiguration(new TestimonialConfiguration());
    }


}
