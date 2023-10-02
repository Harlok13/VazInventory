using Microsoft.EntityFrameworkCore;

using Inventory.DAL.Entities;

namespace Inventory.DAL.ExcelToDatabase;

public class ExcelToDatabase : IExcelToDatabase
{
    public async Task AddDataToContextAsync(DbContext context, IEntity? entity)
    {
        if (entity != null)
        {
            await context.AddAsync(entity);
        }
    }
}