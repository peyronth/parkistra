namespace ParkIstra.Contexts.Main.Configurations;

public class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
{
    public void Configure(EntityTypeBuilder<ApplicationUserClaim> entity)
    {
        _ = entity.ToTable("AspNetUserClaims"/*, t => t.IsTemporal()*/);

        _ = entity.Property(e => e.Description)
            .IsRequired();
    }
    
}
