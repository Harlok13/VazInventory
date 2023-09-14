using Microsoft.EntityFrameworkCore;

namespace Inventory.Entities;

public class Server : IEntities
{
    public int Id { get; set; }

    [Comment("FQDN")] 
    [Column(TypeName = "VARCHAR"), StringLength(32), Required]
    public string Domain { get; set; } = null!;
    
    public int ContourId { get; set; }
    public Contour? Contour { get; set; }
    
    public string? ServerOsId { get; set; }
    public ServerOs? ServerOs { get; set; }
    
    public int? ServerApplicationId { get; set; }
    public ServerApplication? ServerApplication { get; set; }

    public int ServerKindId { get; set; }
    public ServerKind? ServerKind { get; set; }
    
    public int? LocationId { get; set; }
    public Location? Location { get; set; } 
    
    public int? InformationSystemId { get; set; }
    public InformationSystem? InformationSystem { get; set; } 
}