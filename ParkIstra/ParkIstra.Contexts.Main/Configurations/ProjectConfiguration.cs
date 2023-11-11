namespace ParkIstra.Contexts.Main.Configurations;

public class ProjectConfiguration: IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> entity)
    {
        _ = entity.ToTable("Project"/*, t => t.IsTemporal()*/);

        _ = entity.Property(e => e.Id)
            .HasColumnName("ProjectId");

        _ = entity.Property(e => e.Name)
            .HasColumnName("Name")
            .IsRequired();

        _ = entity.Property(e => e.Description)
            .HasColumnName("Description")
            .IsRequired();

    }
}

