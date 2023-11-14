namespace ParkIstra.Contexts.Main.Configurations;

public class TestimonialsConfiguration: IEntityTypeConfiguration<Testimonials>
{
    public void Configure(EntityTypeBuilder<Testimonials> entity)
    {
        _ = entity.ToTable("Testimonials"/*, t => t.IsTemporal()*/);

        _ = entity.Property(e => e.Id)
            .HasColumnName("TestimonialsId");

        _ = entity.Property(e => e.Content)
            .HasColumnName("Content")
            .IsRequired();
        
        _ = entity.Property(e => e.Author)
            .HasColumnName("Author")
            .IsRequired();
        
        _ = entity.Property(e => e.Country)
            .HasColumnName("Country")
            .IsRequired();
    }
}

