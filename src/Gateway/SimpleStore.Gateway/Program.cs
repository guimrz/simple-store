using Ocelot.DependencyInjection;
using Ocelot.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
{
    configurationBuilder.AddOcelot("Configuration", hostBuilderContext.HostingEnvironment);
});
builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseOcelot();
app.Run();
