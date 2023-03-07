﻿using Microsoft.EntityFrameworkCore;
using MiniERP.API.Data;

namespace MiniERP.API.Configuration
{
    public static class Configuration
    {
        public static IWebHostBuilder BaseApiConfiguration(this IWebHostBuilder builder)
            => builder.ConfigureServices(ConfigureServices())
                      .Configure(ConfigureApp());

        private static Action<WebHostBuilderContext, IApplicationBuilder> ConfigureApp()
            => (context, app) =>
            {
                // Configure the HTTP request pipeline.
                if (context.HostingEnvironment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseRouting();
                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.UseEndpoints(builder => builder.MapControllers());
            };

        private static Action<WebHostBuilderContext, IServiceCollection> ConfigureServices()
            => (context, service) =>
            {
                // ConfigureServices
                service.AddDbContext<MiniERPDbContext>(options => options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                // Add services to the container.
                service.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                service.AddEndpointsApiExplorer();
                service.AddSwaggerGen();
                service.ResolveDependencies();
            };
    }
}