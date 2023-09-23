using Inventory.DAL.Entities;

namespace Inventory.DAL.Cache;

public interface ICacheData
{
    public bool TryAdd(string key, IEntities value);
    public IEntities Get(string key);
}