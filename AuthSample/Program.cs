using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite("DataSource=app.db"));
builder.Services.AddIdentityCore<MyUser>().AddEntityFrameworkStores<AppDbContext>().AddApiEndpoints();]


//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapIdentityApi<MyUser>();

app.MapGet("/", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}").RequireAuthorization();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.Run();


class MyUser : IdentityUser { }

class AppDbContext : IdentityDbContext<MyUser> 
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}