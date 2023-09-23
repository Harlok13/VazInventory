using Inventory.DAL.Cache;
using Inventory.DAL.Context;
using Inventory.DAL.ExcelToDatabase;

namespace Inventory.BL.ExcelProcessing;

public interface IXlsxProcessing
{
    public Task StartAsync(InventoryContext context, IExcelToDatabase excelToDatabase, ICacheData cacheData);
}