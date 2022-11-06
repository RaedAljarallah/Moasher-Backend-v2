using Moasher.Authentication;
using Moasher.Authentication.Core.Persistence;

var builder = WebApplication.CreateBuilder(args);
var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

if (!builder.Environment.IsDevelopment())
{
    DbContextInitializer.InitializeConfig(app);
}

app.Run();
