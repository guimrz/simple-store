using SimpleStore.Core.Mvc.Middlewares;
using SimpleStore.Services.Basket.Application;
using SimpleStore.Services.Basket.Grpc;
using SimpleStore.Services.Basket.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBasketApplication();
builder.Services.AddBasketGrpc();
builder.Services.AddBasketRepository();
builder.Services.AddDistributedMemoryCache();

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

app.Run();
