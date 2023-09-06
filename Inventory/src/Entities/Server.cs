using Microsoft.EntityFrameworkCore;

namespace Inventory.Entities;

public class Server
{
    public int Id { get; set; }

    [Comment("FQDN")] 
    [Column(TypeName = "VARCHAR"), StringLength(32), Required]
    public string Domain { get; set; } = null!;
    
    public int ContourId { get; set; }
    public Contour Contour { get; set; } = null!;
    
    public int ServerOsId { get; set; }
    public ServerOs ServerOs { get; set; } = null!;
    
    public int ServerApplicationId { get; set; }
    public ServerApplication ServerApplication { get; set; } = null!;

    public int ServerKindId { get; set; }
    public ServerKind ServerKind { get; set; } = null!;
    
    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;
}