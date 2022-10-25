using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
{
    configurationBuilder.AddOcelot("Configuration", hostBuilderContext.HostingEnvironment);
    configurationBuilder.AddEnvironmentVariables();
});
builder.Services.AddOcelot().AddConsul();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseOcelot();
app.Run();
