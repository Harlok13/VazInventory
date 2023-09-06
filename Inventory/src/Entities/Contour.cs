namespace Inventory.Entities;

public class Contour
{
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR"), StringLength(32), Required]
    public string Name { get; set; } = null!;

    public ICollection<Server> Servers = null!;
}