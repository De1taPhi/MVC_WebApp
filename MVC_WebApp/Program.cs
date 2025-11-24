using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MVC_WebApp.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();



builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var connectionString = builder.Configuration.GetConnectionString("MainDb")
    ?? throw new InvalidOperationException("Connection string 'MainDb' not found.");


builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(connectionString));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
    try
    {
        dbContext.Database.Migrate();
        Console.WriteLine("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
        // Log the error or handle it appropriately
    }
}

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
