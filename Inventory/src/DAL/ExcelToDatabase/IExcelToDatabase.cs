using Inventory.DAL.Context;
using Inventory.DAL.Entities;

namespace Inventory.DAL.ExcelToDatabase;

public interface IExcelToDatabase
{
    public Task AddDataToContextAsync(InventoryContext context, IEntities entities);
}