namespace Inventory.Entities;

public class ServerApplication
{
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR"), StringLength(32), Required]
    public string Name { get; set; } = null!;
    public string? Version { get; set; }

    public ICollection<Server> Servers { get; set; } = null!;
}