namespace ParkIstra.Contexts.Main.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> entity)
    {
        _ = entity.ToTable("Project"/*, t => t.IsTemporal()*/);

        _ = entity.Property(e => e.Id)
            .HasColumnName("ImageId");

        _ = entity.Property(e => e.Url)
            .HasColumnName("Url")
            .IsRequired();

        _ = entity.HasOne(d => d.Project)
            .WithMany(p => p.Images)
            .HasForeignKey(d => d.ProjectId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Images_Project");

    }
}

