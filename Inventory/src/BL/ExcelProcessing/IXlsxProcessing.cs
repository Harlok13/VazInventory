using Microsoft.EntityFrameworkCore;

using Inventory.DAL.Cache;
using Inventory.DAL.ExcelToDatabase;

namespace Inventory.BL.ExcelProcessing;

public interface IXlsxProcessing
{
    public Task StartAsync(DbContext context, IExcelToDatabase excelToDatabase, ICacheData cacheData);
}