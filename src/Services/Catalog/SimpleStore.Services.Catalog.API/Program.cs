using Microsoft.AspNetCore.Mvc;
using SimpleStore.Core.EntityFrameworkCore.Extensions;
using SimpleStore.Core.EntityFrameworkCore.SqlServer.Extensions;
using SimpleStore.Core.Mvc;
using SimpleStore.Core.Mvc.Middlewares;
using SimpleStore.Services.Catalog.Application;
using SimpleStore.Services.Catalog.Grpc;
using SimpleStore.Services.Catalog.Repository;
using SimpleStore.Services.Catalog.Repository.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    options.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(new ErrorResponse(context.ModelState)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCatalogApplication();
builder.Services.AddCatalogGrpcServer();
builder.Services.AddCatalogRepository();
builder.Services.AddSqlDatabase<CatalogDbContext>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.UseCatalogGrpc();

await app.MigrateDatabaseAsync<CatalogDbContext>();

app.Run();
