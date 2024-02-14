using AplikacjaLaby.Classes;
using AplikacjaLaby.Models.Services;
using AplikacjaLabyData;
using Microsoft.AspNetCore.Identity;

namespace AplikacjaLaby
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            //builder.Services.AddSingleton<IBookService, MemoryBookService>(); // Register SERVICE for books (memory)
            builder.Services.AddSingleton<ITimeProvider, CustomTimeProvider>(); // Register TimeProvider Service


            builder.Services.AddDbContext<AppDbContext>(); // Register DB context

            //Add Razor pages (neede for Identity views to work)
            builder.Services.AddRazorPages();

            // Register Identity
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>() // Add Roles
                .AddEntityFrameworkStores<AppDbContext>();

            // Added, don't know why tbh
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();


            builder.Services.AddTransient<IBookService, SQLiteBookService>(); // Register SERVICE for book (SQLite)

            var app = builder.Build();

            // Added with Identity
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapRazorPages();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseMiddleware<LastVisitCookie>(); // Register custom middleware in correct place! (after routing and before authotization will use it only when route (address) is ok)
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
