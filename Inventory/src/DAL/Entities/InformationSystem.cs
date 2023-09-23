namespace Inventory.DAL.Entities;

public class InformationSystem : IEntities
{
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR"), StringLength(15), Required]
    public string Code { get; set; } = null!;
    
    [Column(TypeName = "VARCHAR"), StringLength(64), Required]
    public string Name { get; set; } = null!;

    public ICollection<Server> Servers { get; set; } = null!;
}