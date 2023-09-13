namespace Inventory.Entities;

public class ServerApplication : IEntities
{
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR"), StringLength(32)]
    public string? Name { get; set; }
    public string? Version { get; set; }

    public ICollection<Server> Servers { get; set; } = null!;
}