namespace ParkIstra.Contexts.Main.Configurations;


public class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
{
    public void Configure(EntityTypeBuilder<ApplicationUserLogin> entity)
    {
        _ = entity.ToTable("AspNetUserLogins"/*, t => t.IsTemporal()*/);
    }
    
}
