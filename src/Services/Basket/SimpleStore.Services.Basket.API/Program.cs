using Microsoft.AspNetCore.Authentication.JwtBearer;
using SimpleStore.Core.Mvc.Middlewares;
using SimpleStore.Services.Basket.Application;
using SimpleStore.Services.Basket.Grpc;
using SimpleStore.Services.Basket.Repository;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBasketApplication();
builder.Services.AddBasketGrpc();
builder.Services.AddBasketRepository();
builder.Services.AddDistributedMemoryCache();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
