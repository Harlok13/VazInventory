using Microsoft.EntityFrameworkCore;

namespace Inventory.Entities;

[Index(nameof(Name))]
[Index(nameof(Version))]
public class ServerOs
{
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR"), StringLength(32), Required]
    public string Name { get; set; } = null!;
    public double? Version { get; set; }

    public ICollection<Server> Servers { get; set; } = null!;
}