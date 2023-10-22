using FoodWaste.DomainServices.IRepositories;
using FoodWaste.Infrastructure.Data;
using FoodWaste.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FoodWaste.Domain;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepository<Package>, PackageRepository>();
builder.Services.AddScoped<IRepository<Student>, StudentRepository>();
builder.Services.AddScoped<IRepository<Cafeteria>, CafeteriaRepository>();
builder.Services.AddScoped<IRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();


builder.Services.AddDbContext<WebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppContext") ?? throw new InvalidOperationException("Connection string 'WebAppContext' not found.")));

builder.Services.AddDbContext<AccountContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AccountContext") ?? throw new InvalidOperationException("Connection string 'IdentityContext' not found.")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AccountContext>().AddDefaultTokenProviders();

builder.Services.AddAuthorization(options => options.AddPolicy("EmployeeOnly", policy => policy.RequireAssertion(context => context.User.HasClaim(c => (c.Type == "Employee")))));
builder.Services.AddAuthorization(options => options.AddPolicy("StudentOnly", policy => policy.RequireAssertion(context => context.User.HasClaim(c => (c.Type == "Student")))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();  
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
