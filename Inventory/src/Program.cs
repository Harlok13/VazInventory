using System.Collections.Immutable;
using Inventory.Data.Context;
using Inventory.ExcelProcessing;

namespace Inventory
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await using var context = new InventoryContext(DbDSN);

            await context.CheckDbConnectionOrThrowAsync();

            var files = Directory.GetFiles(PathToXlsxFiles);

            var filesList = files.Where(f => f.EndsWith("xlsx")).ToImmutableArray();

            foreach (var file in filesList)
            {
                var xlsx = new XlsxProcessing(PathToXlsxFiles, file);

                await xlsx.SetServersAsync(context, InventoryContext.CacheDict);
            }

            await context.SaveChangesAsync();
        }
    }
}