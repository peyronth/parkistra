using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ParkIstra.Contexts.Main.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> entity)
    {
        _ = entity.ToTable("AspNetUsers"/*, t => t.IsTemporal()*/);
                
        _ = entity.HasMany(e => e.Claims)
            .WithOne(e => e.User)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .IsRequired();

        // Each User can have many UserLogins
        _ = entity.HasMany(e => e.Logins)
            .WithOne(e => e.User)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .IsRequired();

        // Each User can have many UserTokens
        _ = entity.HasMany(e => e.Tokens)
            .WithOne(e => e.User)
            .HasForeignKey(ut => ut.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .IsRequired();

        // Each User can have many entries in the UserRole join table
        _ = entity.HasMany(e => e.UserRoles)
            .WithOne(e => e.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .IsRequired();

        _ = entity
            .HasOne(b => b.UserInfo)
            .WithOne(i => i.User)
            .HasForeignKey<UserInfo>(b => b.UserId);

    }
}

