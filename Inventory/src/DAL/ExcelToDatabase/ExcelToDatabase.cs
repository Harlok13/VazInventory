using Inventory.DAL.Context;
using Inventory.DAL.Entities;

namespace Inventory.DAL.ExcelToDatabase;

public class ExcelToDatabase : IExcelToDatabase
{
    public async Task AddDataToContextAsync(InventoryContext context, IEntities? entity)
    {
        if (entity != null)
        {
            await context.AddAsync(entity);
        }
    }
}