using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Applications.Identity.WebApp;
using SimpleStore.Applications.Identity.WebApp.Data;
using SimpleStore.Applications.Identity.WebApp.Models;
using SimpleStore.Core.EntityFrameworkCore.Extensions;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;

        options.EmitStaticAudienceClaim = true;
    })
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddAspNetIdentity<ApplicationUser>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages()
    .RequireAuthorization();

SeedData.EnsureSeedData(app);
await app.MigrateDatabaseAsync<ApplicationDbContext>();

app.Run();