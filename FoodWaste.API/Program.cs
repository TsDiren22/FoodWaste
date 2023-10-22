using FoodWaste.API.GraphQl;
using FoodWaste.Domain;
using FoodWaste.DomainServices.IRepositories;
using FoodWaste.Infrastructure.Data;
using FoodWaste.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Repo injection
services.AddSingleton<IRepository<Package>, PackageRepository>();
services.AddSingleton<IRepository<Student>, StudentRepository>();


// Db injection
services.AddDbContext<WebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppContext") ?? throw new InvalidOperationException("Connection string 'WebAppContext' not found.")), ServiceLifetime.Singleton);

services.AddDbContext<AccountContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AccountContext") ?? throw new InvalidOperationException("Connection string 'IdentityContext' not found.")), ServiceLifetime.Singleton);


services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AccountContext>().AddDefaultTokenProviders();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddGraphQLServer().AddQueryType<Query>();

// Configure JWT usage.
services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters.ValidateAudience = false;
    options.TokenValidationParameters.ValidateIssuer = false;
    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["BearerTokens:Key"]));
});

services.AddAuthorization(options =>
{
    options.AddPolicy("JwtPolicy", policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGraphQL("/graphql").RequireAuthorization("JwtPolicy");
app.Run();