using Inventory.Entities;
using Inventory.ExcelProcessing;
using static Inventory.Settings;

namespace Inventory
{
    class Program
    {
        private static readonly Dictionary<string, IEntities> CacheDict = new();

        public static async Task Main(string[] args)
        {
            await using var context = new Context(DbDSN);

            await context.CheckDbConnectionOrThrowAsync();

            var files = Directory.GetFiles(PathToXlsxFiles);

            var filesList = files.Where(f => f.EndsWith("xlsx")).ToList();

            foreach (var file in filesList)
            {
                var xlsx = new XlsxProcessing(PathToXlsxFiles, file);

                await xlsx.SetServers(context, CacheDict);
            }

            await context.SaveChangesAsync();
        }
    }
}