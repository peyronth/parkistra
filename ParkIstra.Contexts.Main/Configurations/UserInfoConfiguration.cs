namespace ParkIstra.Contexts.Main.Configurations;

public class UserInfoConfiguration: IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> entity)
    {
        _ = entity.ToTable("UserInfo"/*, t => t.IsTemporal()*/);

        _ = entity.Property(e => e.UserInfoId)
            .HasColumnName("UserInfoId");

        _ = entity.Property(e => e.UserType)
            .HasColumnName("UserType")
            .IsRequired();

        _ = entity.Property(e => e.IdentifikacioniBroj)
            .HasColumnName("IdentifikacioniBroj")
            .HasMaxLength(13)
            .IsRequired();

        _ = entity.Property(e => e.Naziv)
            .HasColumnName("Naziv")
            .IsRequired()
            .HasMaxLength(50);

        _ = entity.Property(e => e.Adresa)
            .HasColumnName("Adresa")
            .IsRequired()
            .HasMaxLength(50);

        _ = entity.Property(e => e.OdgovornoLice)
            .IsRequired(false)
            .HasColumnName("OdgovornoLice");

        _ = entity.Property(e => e.KontaktPodaci)
            .HasColumnName("KontaktPodaci");

        _ = entity.Property(e => e.Checked)
            .IsRequired()
            .HasColumnName("Checked");

        _ = entity.Property(e => e.UserId)
            .HasColumnName("UserId");

    }
}

