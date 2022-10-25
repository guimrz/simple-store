using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Configure app configuration
    builder.WebHost.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
    {
        configurationBuilder.AddOcelot("Configuration", hostBuilderContext.HostingEnvironment);
        configurationBuilder.AddEnvironmentVariables();
    });

    // Configure logger
    builder.Host.UseSerilog((context, configuration) =>
    {
        configuration.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(context.Configuration);
    });

    builder.Services.AddOcelot()
        .AddConsul();

    var app = builder.Build();

    // Configure serilog for requests
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    app.UseOcelot();
    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Unhandled exception");
}
finally
{
    Log.Information("Shutdown completed");
    Log.CloseAndFlush();
}
