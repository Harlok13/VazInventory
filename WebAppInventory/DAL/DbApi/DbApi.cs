using Inventory.DAL.Context;
using Inventory.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebAppInventory.DAL.DbApi;

public class DbApi
{
    private readonly WebAppInventoryContext _dbContext;

    public DbApi(WebAppInventoryContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<InformationSystem> GetAllInformationSystems() => _dbContext.InformationSystems.ToList();
    
    public async Task<InformationSystem?> GetInformationSystemAsync(int id) =>
        await _dbContext.InformationSystems.FirstOrDefaultAsync(infSys => infSys.Id == id);

    public async Task<InformationSystem?> EditInformationSystemAsync(InformationSystem infSys)
    {
        var newInfSys = await _dbContext.InformationSystems.FirstOrDefaultAsync(i => i.Id == infSys.Id);

        if (newInfSys == null) return null;
        
        newInfSys.Code = infSys.Code;
        newInfSys.Name = infSys.Name;
        
        await _dbContext.SaveChangesAsync();
        return newInfSys;
    }
    
    public async Task<InformationSystem?> DeleteInformationSystemAsync(int id)
    {
        var infSys = await _dbContext.InformationSystems.FirstOrDefaultAsync(i => i.Id == id);
        
        if (infSys == null) return null;
        
        _dbContext.InformationSystems.Remove(infSys);
         await _dbContext.SaveChangesAsync();
         
        return infSys;
    }

    public async Task<InformationSystem?> CreateInformationSystemAsync(InformationSystem? infSys)
    {
        if (infSys == null) return null;
        
        await _dbContext.InformationSystems.AddAsync(infSys);
        await _dbContext.SaveChangesAsync();

        return infSys;
    }
}