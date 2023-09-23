namespace Inventory.DAL.Entities.Lanit;

public class Lanit : IEntities
{
    public int Id { get; set; }

    public string? Name { get; set; } = null;
    public string? Code { get; set; } = null;
    public string? Domain { get; set; } = null;
    public string? ServersKind { get; set; } = null;
    public string? Integrations { get; set; } = null;
}