using Inventory.DAL.Entities;
using Inventory.DAL.Entities.Lanit;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DAL.Context;


public sealed class WebAppInventoryContext : DbContext
{
    public DbSet<InformationSystem> InformationSystems { get; set; } = null!;
    public DbSet<Server> Servers { get; set; } = null!;
    public DbSet<ServerApplication> ServerApplications { get; set; } = null!;
    public DbSet<ServerKind> ServerKinds { get; set; } = null!;
    public DbSet<ServerOs> ServersOs { get; set; } = null!;
    public DbSet<Contour> Contours { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;

    public DbSet<Lanit> Lanit { get; set; } = null!;
    
    public WebAppInventoryContext(DbContextOptions<WebAppInventoryContext> options) : base(options)
    {
        if (Settings.RecreatingDb)  // turn off for perf
        {
            // Database.EnsureDeleted();  // relocate to up
            Database.EnsureCreated();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lanit>().ToTable(nameof(Lanit), $"{nameof(Lanit)}Schema");
        modelBuilder.Entity<InformationSystem>()
            .HasMany(i => i.Servers)
            .WithOne(s => s.InformationSystem)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public async Task CheckDbConnectionOrThrowAsync()  // relocate to up
    {
        bool isAvailable = await Database.CanConnectAsync();
        if (!isAvailable) throw new Exception("Can't connect to DB");
        
        WriteLine("Database available");
    }
}