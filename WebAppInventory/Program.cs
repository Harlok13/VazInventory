using Inventory.DAL.Context;
using Inventory.DAL.Entities;

using WebAppInventory.DAL.DbApi;
using WebAppInventory.Extensions;
using WebAppInventory.Handlers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInventoryDbContext(builder);
builder.Services.AddDbApi();

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/{0}");
app.UseFileServerWithOptions();

app.MapGet("/api/information_systems/{id:int}", async (DbApi dbApi, int id) =>
    await ApiHandler.ResultResponseAsync(id, dbApi.GetInformationSystemAsync));

app.MapGet("/api/information_systems", ApiHandler.GetAllInformationSystemsHandler);

app.MapPut("/api/information_systems", async (DbApi dbApi, InformationSystem infSys) =>
    await ApiHandler.ResultResponseAsync(infSys, dbApi.EditInformationSystemAsync));

app.MapDelete("/api/information_systems/{id:int}", async (DbApi dbApi, int id) => 
    await ApiHandler.ResultResponseAsync(id, dbApi.DeleteInformationSystemAsync));

app.MapPost("/api/information_systems", async (DbApi dbApi, InformationSystem infSys) =>
    await ApiHandler.ResultResponseAsync(infSys, dbApi.CreateInformationSystemAsync));

app.MapGet("/tables", async context =>
    await context.Response.SendFileAsync("wwwroot/html/table.html"));

app.Map("/", async context =>
    await context.Response.SendFileAsync("wwwroot/html/index.html"));

app.MapPost("/uploads", async (HttpContext context, WebAppInventoryContext inventoryDbContext) =>
    await ApiHandler.ExcelHandlerAsync(context, inventoryDbContext));

app.Run();