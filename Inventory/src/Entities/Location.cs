namespace Inventory.Entities;

public class Location
{
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR"), StringLength(64), Required]
    public string LocationIn { get; set; } = null!;

    public ICollection<Server> Servers { get; set; } = null!;
}