using DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.EF.Config
{
    public static class EntityFrameworkConfig
    {
        public static void ConfigureEntityFramework(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<MyDbContext>(
                options => options.UseSqlite(dbConnectionString));
            //                .UseLazyLoadingProxies());
            //services.AddEntityFrameworkProxies();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
