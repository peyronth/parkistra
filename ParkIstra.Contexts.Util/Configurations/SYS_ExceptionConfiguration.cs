namespace ParkIstra.Contexts.Util.Configurations;

public class SYS_ExceptionConfiguration : IEntityTypeConfiguration<SYS_Exception>
{
    public void Configure(EntityTypeBuilder<SYS_Exception> entity)
    {
        _ = entity.ToTable("SYS_Exception"/*, t => t.IsTemporal()*/);

        _ = entity.Property(e => e.Id)
           .HasColumnName("SYS_ExceptionID");

        _ = entity.Property(e => e.Tip)
            .HasColumnName("Tip")
            .HasMaxLength(100);

        _ = entity.Property(e => e.Opis)
            .HasColumnName("Opis")
            .HasColumnType("text");

        _ = entity.Property(e => e.UserId)
            .HasColumnName("UserID");


    }
}
