using Microsoft.EntityFrameworkCore;

namespace Inventory.DAL.Entities;

public class ServerKind : IEntities
{
    public int Id { get; set; }

    [Comment("Example: API")]
    [Column(TypeName = "VARCHAR"), StringLength(16), Required]
    public string KindName { get; set; } = null!;

    public ICollection<Server> Servers { get; set; } = null!;
}