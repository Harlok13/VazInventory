using Inventory.DAL.Entities;
using Inventory.DAL.Entities.Lanit;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DAL.Context;

public sealed class InventoryContext : DbContext
{
    private readonly string _connectionString;
    
    // public static readonly Dictionary<string, IEntities> CacheDict = new();
    
    public DbSet<InformationSystem> InformationSystems { get; set; } = null!;
    public DbSet<Server> Servers { get; set; } = null!;
    public DbSet<ServerApplication> ServerApplications { get; set; } = null!;
    public DbSet<ServerKind> ServerKinds { get; set; } = null!;
    public DbSet<ServerOs> ServersOs { get; set; } = null!;
    public DbSet<Contour> Contours { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;

    public DbSet<Lanit> Lanit { get; set; } = null!;
    
    public InventoryContext(string connectionString)
    {
        _connectionString = connectionString;
        
        if (RecreatingDb)  // turn off for perf
        {
            Database.EnsureDeleted();  // relocate to up
            Database.EnsureCreated();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lanit>().ToTable(nameof(Lanit), $"{nameof(Lanit)}Schema");
    }

    public async Task CheckDbConnectionOrThrowAsync()  // relocate to up
    {
        bool isAvailable = await Database.CanConnectAsync();
        if (!isAvailable) throw new Exception("Can't connect to DB");
        
        WriteLine("Database available");
    }
}