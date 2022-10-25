using Microsoft.AspNetCore.Authentication.JwtBearer;
using SimpleStore.Core.Mvc.Middlewares;
using SimpleStore.Core.ServiceRegistry.Consul.Extensions;
using SimpleStore.Core.ServiceRegistry.Extensions;
using SimpleStore.Services.Basket.Grpc;
using SimpleStore.Services.Basket.Application;
using SimpleStore.Services.Basket.Repository;

var builder = WebApplication.CreateBuilder(args);

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
