using Inventory.DAL.Entities;

namespace Inventory.DAL.Cache;

public class CacheData : ICacheData 
{
    private readonly Dictionary<string, IEntities> _cacheDict = new();

    public bool TryAdd(string key, IEntities value) => _cacheDict.TryAdd(key, value);

    public IEntities Get(string key) => _cacheDict[key];
}