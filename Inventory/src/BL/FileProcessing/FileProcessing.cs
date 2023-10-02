using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;

using Inventory.BL.CustomExceptions;
using Inventory.BL.ExcelProcessing;
using Inventory.DAL.Cache;
using Inventory.DAL.ExcelToDatabase;

namespace Inventory.BL.FileProcessing;

public class FileProcessing : IFileProcessing
{
    public async Task StartAsync(
        DbContext context,
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

        if (filesArray.Length == 0)
            throw new FileMissingException(
                "It looks like you forgot to specify the correct path to the directory with files or the directory is empty.",
                DateTime.Now
            );

        return filesArray!;
    }

    private async Task SetDataAsync(
        DbContext context,
        IExcelToDatabase excelToDatabase, string pathToFiles,
        ICacheData cacheData
    )
    {
        foreach (var xlsxProcessing in GetFiles(pathToFiles).Select(file => new XlsxProcessing(pathToFiles, file)))
        {
            await xlsxProcessing.StartAsync(context, excelToDatabase, cacheData);
        }
    }
}