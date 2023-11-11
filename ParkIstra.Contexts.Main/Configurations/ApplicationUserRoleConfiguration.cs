namespace ParkIstra.Contexts.Main.Configurations;

public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> entity)
    {
        _ = entity.ToTable("AspNetUserRoles"/*, t => t.IsTemporal()*/);
    }
}

