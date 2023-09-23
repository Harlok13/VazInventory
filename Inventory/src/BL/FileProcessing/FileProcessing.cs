using System.Collections.Immutable;
using Inventory.BL.ExcelProcessing;
using Inventory.DAL.Cache;
using Inventory.DAL.Context;
using Inventory.DAL.ExcelToDatabase;

namespace Inventory.BL.FileProcessing;

class FileProcessing : IFileProcessing
{
    public async Task StartAsync(
        InventoryContext context,
        IExcelToDatabase excelToDatabase,
        ICacheData cacheData,
        ICollection<string> directoriesForParsing
    )
    {
        foreach (var pathToFiles in directoriesForParsing)
        {
            await SetDataAsync(context, excelToDatabase, pathToFiles, cacheData);
        }
    }

    private ImmutableArray<string> GetFiles(string pathToFiles)
    {
        var filesArray = Directory.GetFiles(pathToFiles)
            .Where(f => f.EndsWith("xlsx", StringComparison.OrdinalIgnoreCase))
            .Select(Path.GetFileName)
            .ToImmutableArray();

        if (filesArray.Length == 0) throw new Exception("Missing files"); // InventoryFilesMissingException

        return filesArray!;
    }

    private async Task SetDataAsync(InventoryContext context, IExcelToDatabase excelToDatabase, string pathToFiles,
        ICacheData cacheData)
    {
        foreach (var file in GetFiles(pathToFiles))
        {
            var xlsx = new XlsxProcessing(pathToFiles, file);

            await xlsx.StartAsync(context, excelToDatabase, cacheData);
        }
    }
}