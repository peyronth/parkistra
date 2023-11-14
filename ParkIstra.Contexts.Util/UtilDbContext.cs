using ParkIstra.Contexts.Util.Configurations;

namespace ParkIstra.Contexts.Util;

public partial class UtilDbContext : DbContext
{
    public UtilDbContext(DbContextOptions<UtilDbContext> options)
        : base(options)
    { }

    public DbSet<SYS_Exception> SYS_Exceptions => Set<SYS_Exception>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        _ = modelBuilder.ApplyConfiguration(new SYS_ExceptionConfiguration());

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}
