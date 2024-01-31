using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Egzamin_Rectangle1.Data;
using Egzamin_Rectangle1.Models;
namespace Egzamin_Rectangle1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Context") ?? throw new InvalidOperationException("Connection string 'Context' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            #region DbSeeder

            var scope = app.Services.CreateScope(); // Create scope of services
            var dbContext = scope.ServiceProvider.GetRequiredService<Context>(); // Call instance of DbContext

            using (dbContext)
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                var mm = new Unit
                {
                    Name = "mm",
                    Multiplier = 1
                };

                var cm = new Unit
                {
                    Name = "cm",
                    Multiplier = 10
                };

                var m = new Unit
                {
                    Name = "m",
                    Multiplier = 1000
                };

                dbContext.Units.Add(mm);
                dbContext.Units.Add(cm);
                dbContext.Units.Add(m);
                dbContext.SaveChanges();
            }

            #endregion

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
