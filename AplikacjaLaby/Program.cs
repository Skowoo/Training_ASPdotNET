using AplikacjaLaby.Models.Services;
using AplikacjaLabyData;

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

            builder.Services.AddTransient<IBookService, SQLiteBookService>(); // Register SERVICE for book (SQLite)

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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
