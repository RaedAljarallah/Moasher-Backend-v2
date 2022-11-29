using Moasher.Application;
using Moasher.Infrastructure;
using Moasher.Persistence;
using Moasher.WebApi;
using Moasher.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebApi(builder.Configuration, builder.Environment);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId("a5567c59-7974-4110-a15e-f7f1449be62f");
        options.OAuthAppName("Moasher API - Swagger");
        options.OAuthUsePkce();
    });
}

app.UseCors("CorsPolicy");
app.UseSecurityHeaders();
app.UseHttpsRedirection();
app.UseInfrastructure();

app.MapControllers();

app.Run();