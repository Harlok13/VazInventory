using Npgsql;

using Inventory.BL.FileProcessing;
using Inventory.DAL.Cache;
using Inventory.DAL.Context;
using Inventory.DAL.ExcelToDatabase;

namespace Inventory;

class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            await using var context = new InventoryContext(DbDSN);
            await context.CheckDbConnectionOrThrowAsync();

            var fileProcessing = new FileProcessing();
            var cacheData = new CacheData();

            await fileProcessing.StartAsync(context, new ExcelToDatabase(), cacheData, DirectoriesForParse);

            await context.SaveChangesAsync();
                
            WriteLine("The program has successfully completed its work.");
        }
        catch (NpgsqlException ex)
        {
            WriteLine(ex.Message);
            throw new Exception("Connection refused! Check the connection string or make sure" +
                                " that the server is running.");
        }
        catch (DirectoryNotFoundException ex)
        {
            WriteLine("Make sure that you have correctly specified the path to the directory with the files!" +
                      $"\n{ex.Message}");
        }
        finally
        {
            WriteLine("The application has terminated.");
        }
    }
}