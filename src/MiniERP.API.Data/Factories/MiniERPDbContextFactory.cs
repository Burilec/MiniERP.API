using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MiniERP.API.Data.Factories
{
    //ReSharper disable once UnusedType.Global
    public sealed class MiniERPDbContextFactory : IDesignTimeDbContextFactory<MiniERPDbContext>
    {
        public MiniERPDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            var optionsBuilder = new DbContextOptionsBuilder<MiniERPDbContext>();
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];

            optionsBuilder.UseSqlServer(connectionString, builder => builder.MigrationsAssembly(typeof(MiniERPDbContext).Assembly.GetName().Name));

            return new MiniERPDbContext(optionsBuilder.Options);
        }
    }
}