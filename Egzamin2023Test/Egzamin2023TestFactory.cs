using System.Data.Common;
using Egzamin2023.Services;
using Egzamin2023Test;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Ezgamin2023Test;

public class Egzamin2023TestFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var defaultTimeProvider = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(IDateProvider)
            );
            services.Remove(defaultTimeProvider);
            services
                .AddSingleton<IDateProvider>(new TestDateProvider(new DateTime(2024,1, 1)));
        });
        builder.UseEnvironment("Development");
    }
}