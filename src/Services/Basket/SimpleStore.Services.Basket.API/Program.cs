using Microsoft.AspNetCore.Authentication.JwtBearer;
using SimpleStore.Core.Mvc.Middlewares;
using SimpleStore.Core.ServiceRegistry.Consul.Extensions;
using SimpleStore.Core.ServiceRegistry.Extensions;
using SimpleStore.Services.Basket.Grpc;
using SimpleStore.Services.Basket.Application;
using SimpleStore.Services.Basket.Repository;
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

    builder.Services.AddControllers();

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

    // Configure basket services
    builder.Services.AddBasketApplication();
    builder.Services.AddBasketGrpc();
    builder.Services.AddBasketRepository();
    builder.Services.AddDistributedMemoryCache();

    // Configure service registry
    builder.Services.AddServiceRegistry(builder.Configuration);
    builder.Services.AddConsulServiceRegistry(builder.Configuration);

    builder.Services.AddHealthChecks();

    // Configure identity
    builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, builder.Configuration.GetSection("JwtBearer"));
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        builder.Configuration.Bind("JwtBearer", options);
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("UserOnly", policy =>
        {
            policy.RequireClaim("sub");
        });
    });

    var app = builder.Build();

    // Configure serilog for requests
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseAuthentication();
    app.UseAuthorization();

    // Configure health checks
    app.UseHealthChecks("/health/status");

    app.MapControllers();

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