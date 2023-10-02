using Microsoft.Extensions.FileProviders;

namespace WebAppInventory.Extensions;

public static class ApplicationBuilderExtension
{
    // private const string Path = @"";
    
    public static void UseFileServerWithOptions(this IApplicationBuilder app)
    {
        app.UseFileServer(new FileServerOptions
        {
            // FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), Path))
        });
    }
}