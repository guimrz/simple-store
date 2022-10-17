using SimpleStore.Core.EntityFrameworkCore.Extensions;
using SimpleStore.Core.EntityFrameworkCore.SqlServer.Extensions;
using SimpleStore.Services.Catalog.Application;
using SimpleStore.Services.Catalog.Repository;
using SimpleStore.Services.Catalog.Repository.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCatalogApplication();
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

app.UseAuthorization();

app.MapControllers();

await app.MigrateDatabaseAsync<CatalogDbContext>();

app.Run();
