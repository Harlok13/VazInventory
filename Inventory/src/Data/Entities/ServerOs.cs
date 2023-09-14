using Microsoft.EntityFrameworkCore;

namespace Inventory.Entities;

public class ServerOs : IEntities
{
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR"), StringLength(32)]
    public string? Name { get; set; } 
    
    [Column(TypeName = "VARCHAR"), StringLength(8)]
    public string? Version { get; set; }

    public ICollection<Server>? Servers { get; set; }
}