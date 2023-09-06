using Inventory.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory;

public sealed class Context : DbContext
{
    public Context()
    {
        if (Settings.RecreatingDb)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Settings.DbDSN);
    }
    
    public DbSet<InformationSystem> InformationSystems { get; set; }
    public DbSet<Server> Servers { get; set; }
    public DbSet<ServerApplication> ServerApplications { get; set; }
    public DbSet<ServerKind> ServerKinds { get; set; }
    public DbSet<ServerOs> ServersOs { get; set; }
    public DbSet<Contour> Contours { get; set; }
    public DbSet<Location> Locations { get; set; }
}