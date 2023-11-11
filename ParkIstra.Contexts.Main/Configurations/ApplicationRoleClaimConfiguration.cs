namespace ParkIstra.Contexts.Main.Configurations;

public class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
{
    public void Configure(EntityTypeBuilder<ApplicationRoleClaim> entity)
    {
        _ = entity.ToTable("AspNetRoleClaims"/*, t => t.IsTemporal()*/);

        _ = entity.Property(e => e.Description)
            .IsRequired();
    }
    
}
