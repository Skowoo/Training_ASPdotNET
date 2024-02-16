using DbFirst2_SQL.Data;
using DbFirst2_SQL.Services;
using Microsoft.EntityFrameworkCore;

namespace DbFirst2_SQL
{
    // Install Microsoft.EntityFrameworkCore
    // Install Microsoft.EntityFrameworkCore.SQLite / .SqlServer
    // Create Context class - with DbSet and ctor passing options to base class
    // Create IService and Service, inject Context to service and declare methods
    // Type in connection string to appsettings.json
    // Register Context and Services here
    // ModifyView - inject service to controller and pass parameters accordingly

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register service
            builder.Services.AddTransient<IService, SqlService>();

            // Register DbContext and pass basic configuration (connection string from json file)
            builder.Services.AddDbContext<Context>(options 
                => options.UseSqlServer(builder.Configuration["Data:App:ConnectionString"]));



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
