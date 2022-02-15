using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookClub2.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookClub2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookClub2Context")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add razor pages
builder.Services.AddRazorPages();

// Add session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.UseSession();

app.Run();
