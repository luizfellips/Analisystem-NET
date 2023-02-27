using Analisystem.Data;
using Analisystem.Filters;
using Analisystem.Helper;
using Analisystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Database");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddEntityFrameworkSqlServer().
	AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserSession, UserSession>();
builder.Services.AddScoped<IEmail, Email>();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


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

app.Use(async (context, next) =>
{
	var currentThreadCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
	currentThreadCulture.NumberFormat = NumberFormatInfo.InvariantInfo;

	Thread.CurrentThread.CurrentCulture = currentThreadCulture;
	Thread.CurrentThread.CurrentUICulture = currentThreadCulture;

	await next();
});

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
