using Inventory.DAL.Cache;
using Inventory.DAL.Context;
using Inventory.DAL.ExcelToDatabase;

namespace Inventory.BL.FileProcessing;

public interface IFileProcessing
{
    public Task StartAsync(
        InventoryContext context,
        IExcelToDatabase excelToDatabase,
        ICacheData cacheData,
        ICollection<string> directoriesForParsing);
}