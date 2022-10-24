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

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.MapInboundClaims = false;
        options.Authority = builder.Configuration.GetValue<string>("IdentityConfiguration:Authority");
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false
        };
        options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("IdentityConfiguration:RequireHttpsMetadata");
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
