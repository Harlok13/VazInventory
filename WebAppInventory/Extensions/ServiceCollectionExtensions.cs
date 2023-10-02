using Microsoft.EntityFrameworkCore;

using Inventory.DAL.Context;
using WebAppInventory.DAL.DbApi;

namespace WebAppInventory.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInventoryDbContext(this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        // TODO: WebAppInventoryContext or...
        services.AddDbContext<WebAppInventoryContext>(options => options.UseNpgsql(connectionString));
    }

    public static void AddDbApi(this IServiceCollection services)
    {
        services.AddScoped<DbApi>();
    }
}