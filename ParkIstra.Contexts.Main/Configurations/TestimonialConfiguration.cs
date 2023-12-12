namespace ParkIstra.Contexts.Main.Configurations;

public class TestimonialConfiguration: IEntityTypeConfiguration<Testimonial>
{
    public void Configure(EntityTypeBuilder<Testimonial> entity)
    {
        _ = entity.ToTable("Testimonial"/*, t => t.IsTemporal()*/);

        _ = entity.Property(e => e.Id)
            .HasColumnName("TestimonialId");

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

