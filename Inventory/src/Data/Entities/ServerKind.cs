using Microsoft.EntityFrameworkCore;

namespace Inventory.Data.Entities;

public class ServerKind : IEntities
{
    public int Id { get; set; }
    
    [Comment("Example: API")]
    [Column(TypeName = "VARCHAR"), StringLength(16)]
    public string? KindName { get; set; }

    public ICollection<Server> Servers { get; set; } = null!;
}