using Moasher.Application;
using Moasher.Infrastructure;
using Moasher.Persistence;
using Moasher.WebApi;

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
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseInfrastructure();

app.MapControllers();

app.Run();