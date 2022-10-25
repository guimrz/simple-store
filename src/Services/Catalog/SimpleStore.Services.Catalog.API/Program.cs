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

var builder = WebApplication.CreateBuilder(args);

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
