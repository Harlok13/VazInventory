namespace Inventory.Data.Entities;

public class Location : IEntities
{
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR"), StringLength(64)]
    public string? LocationIn { get; set; }

    public ICollection<Server> Servers { get; set; } = null!;
}