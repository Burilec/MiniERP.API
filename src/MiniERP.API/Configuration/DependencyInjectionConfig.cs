using MiniERP.API.Business.Interfaces;
using MiniERP.API.Business.Services;
using MiniERP.API.Data;
using MiniERP.API.Data.Interfaces;
using MiniERP.API.Data.Repositories;

namespace MiniERP.API.Configuration
{
    internal static class DependencyInjectionConfig
    {
        internal static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MiniERPDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductService, ProductService>();
        }
    }
}