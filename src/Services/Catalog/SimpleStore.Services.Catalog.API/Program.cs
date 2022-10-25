using Microsoft.AspNetCore.Mvc;
using SimpleStore.Core.EntityFrameworkCore.Extensions;
using SimpleStore.Core.EntityFrameworkCore.SqlServer.Extensions;
using SimpleStore.Core.Mvc;
using SimpleStore.Core.Mvc.Middlewares;
using SimpleStore.Core.ServiceRegistry.Consul.Extensions;
using SimpleStore.Core.ServiceRegistry.Extensions;
using SimpleStore.Services.Catalog.Application;
using SimpleStore.Services.Catalog.Grpc;
using SimpleStore.Services.Catalog.Repository;
using SimpleStore.Services.Catalog.Repository.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up...");

try
{

    var builder = WebApplication.CreateBuilder(args);


    // Configure logger
    builder.Host.UseSerilog((context, configuration) =>
    {
        configuration.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(context.Configuration);
    });

    builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    options.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(new ErrorResponse(context.ModelState)));

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
    });

    // Configure catalog services
    builder.Services.AddCatalogApplication();
    builder.Services.AddCatalogGrpcServer();
    builder.Services.AddCatalogRepository();
    builder.Services.AddSqlDatabase<CatalogDbContext>(builder.Configuration);

    // Configure service registry
    builder.Services.AddServiceRegistry(builder.Configuration);
    builder.Services.AddConsulServiceRegistry(builder.Configuration);

    builder.Services.AddHealthChecks();

    var app = builder.Build();

    // Configure serilog for requests
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseHttpsRedirection();

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseAuthorization();

    // Configure health checks
    app.UseHealthChecks("/health/status");

    app.MapControllers();

    app.UseCatalogGrpc();
    await app.MigrateDatabaseAsync<CatalogDbContext>();

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
