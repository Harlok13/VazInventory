using Inventory.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory;

public sealed class Context : DbContext
{
    private readonly string _connectionString;
    
    public DbSet<InformationSystem> InformationSystems { get; set; } = null!;
    public DbSet<Server> Servers { get; set; } = null!;
    public DbSet<ServerApplication> ServerApplications { get; set; } = null!;
    public DbSet<ServerKind> ServerKinds { get; set; } = null!;
    public DbSet<ServerOs> ServersOs { get; set; } = null!;
    public DbSet<Contour> Contours { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;
    
    public Context(string connectionString)
    {
        _connectionString = connectionString;
        
        if (Settings.RecreatingDb)  // turn off for perf
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    public async Task CheckDbConnectionOrThrowAsync()
    {
        bool isAvailable = await Database.CanConnectAsync();
        if (!isAvailable) throw new Exception("Can't connect to DB");
        
        WriteLine("Database available");
    }
    
}