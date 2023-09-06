using Microsoft.EntityFrameworkCore;

namespace Inventory.Entities;

public class ServerKind
{
    public int Id { get; set; }
    
    [Comment("Example: API")]
    [Column(TypeName = "VARCHAR"), StringLength(16), Required]
    public string KindName { get; set; } = null!;

    public ICollection<Server> Servers { get; set; } = null!;
}