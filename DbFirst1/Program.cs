using DbFirst1.Data;
using DbFirst1.Services;

namespace DbFirst1
{
    // DBFirst:
    // Install Microsoft.EntityFrameworkCore
    // Install Microsoft.EntityFrameworkCore.SQLite / .SqlServer
    // Create class reprezenting product (important - Naming convention!)
    // Create Service repository (or not) and specify first method
    // Create DbContext class (depening on technology) - add DbSet<> and constructor
    // Create service and inject DbContext, implement methods
    // Register DbContext class here
    // Register serice here
    // Add Service to controller and implement visualization

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add SQLite Context - access declared in DbContext class
            builder.Services.AddDbContext<ApplicationDbContext>();

            // Register SQLite Service
            builder.Services.AddScoped<IProductService, SQLiteService>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
