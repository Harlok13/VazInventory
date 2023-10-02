using Microsoft.EntityFrameworkCore;

using Inventory;
using Inventory.BL.FileProcessing;
using Inventory.DAL.Cache;
using Inventory.DAL.Context;
using Inventory.DAL.Entities;
using Inventory.DAL.ExcelToDatabase;

using WebAppInventory.DAL.DbApi;

namespace WebAppInventory.Handlers;

public static class ApiHandler
{
    public static async Task ExcelHandlerAsync(HttpContext context, DbContext inventoryDbContext)
    {
        // TODO: ref
        IFormFileCollection filesCollection = context.Request.Form.Files;

        var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
        Settings.PathToInventoryXlsxFiles = uploadPath;

        Directory.CreateDirectory(uploadPath);

        foreach (var file in filesCollection)
        {
            string fullPath = $"{uploadPath}/{file.FileName}";

            await using var fileStream = new FileStream(fullPath, FileMode.Create);

            await file.CopyToAsync(fileStream);
        }
        
        if (inventoryDbContext is WebAppInventoryContext dbContext)
            await dbContext.CheckDbConnectionOrThrowAsync();

        var fileProcessing = new FileProcessing();
        var cacheData = new CacheData();

        await fileProcessing.StartAsync(inventoryDbContext, new ExcelToDatabase(), cacheData, new List<string>() { uploadPath });

        await inventoryDbContext.SaveChangesAsync();  // TODO: DAL layer here!

        await context.Response.WriteAsync("Done!");
    }

    public static IResult GetAllInformationSystemsHandler(HttpContext context, DbApi dbApi)
    {
        var res = dbApi.GetAllInformationSystems();

        context.Response.ContentType = "text/html; charset=utf8";

        return Results.Json(res);
    }

    public static async Task<IResult> ResultResponseAsync<TFrontendData>(
        TFrontendData frontendData,
        Func<TFrontendData, Task<InformationSystem?>> dbActionDelegate)
    {
        var infSys = await dbActionDelegate(frontendData);

        if (infSys == null) return Results.NotFound(new { message = "There is no such object." });
        
        return Results.Json(infSys);
    }
}