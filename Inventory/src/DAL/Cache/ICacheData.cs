using Inventory.DAL.Entities;

namespace Inventory.DAL.Cache;

public interface ICacheData
{
    public bool TryAdd<TType>(string key, TType value);
    public IEntity Get<TType>(string key);
}