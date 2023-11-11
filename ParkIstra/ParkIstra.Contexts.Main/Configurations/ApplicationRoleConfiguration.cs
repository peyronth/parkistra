namespace ParkIstra.Contexts.Main.Configurations;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> entity)
    {
        _ = entity.ToTable("AspNetRoles"/*, t => t.IsTemporal()*/);

        _ = entity.HasMany(e => e.UserRoles)
            .WithOne(e => e.Role)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .IsRequired();

        _ = entity.HasMany(e => e.Claims)
            .WithOne(e => e.Role)
            .HasForeignKey(rc => rc.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .IsRequired();
    }
    
}
