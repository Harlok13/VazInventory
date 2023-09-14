namespace Inventory.Data.Entities;

public class Contour : IEntities
{
    public int Id { get; set; }
    
    [Column(TypeName = "VARCHAR"), StringLength(32), Required]
    public string? Name { get; set; }

    public ICollection<Server> Servers = null!;
}