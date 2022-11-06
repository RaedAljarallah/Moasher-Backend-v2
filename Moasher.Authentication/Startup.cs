using Microsoft.AspNetCore.DataProtection;
using Moasher.Authentication.Core.IdentityServer;
using Moasher.Authentication.Core.Persistence;
using Moasher.Authentication.Core.Identity;

namespace Moasher.Authentication;

internal static class Startup
{
    internal static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddIdentity(builder.Configuration);
        builder.Services.AddIdentityServer(builder.Configuration, builder.Environment);
        return builder.Build();
    }

    internal static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}