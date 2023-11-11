namespace ParkIstra.Contexts.Main.Configurations;

public class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
{
    public void Configure(EntityTypeBuilder<ApplicationUserToken> entity)
    {
        _ = entity.ToTable("AspNetUserTokens"/*, t => t.IsTemporal()*/);
    }
    
}
