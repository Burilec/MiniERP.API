using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MiniERP.API.Data.Factories
{
    // ReSharper disable once UnusedType.Global
    public class MiniERPDbContextFactory : IDesignTimeDbContextFactory<MiniERPDbContext>
    {
        public MiniERPDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            var optionsBuilder = new DbContextOptionsBuilder<MiniERPDbContext>();
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x => x.MigrationsAssembly(typeof(MiniERPDbContext).Assembly.GetName().Name));

            return new MiniERPDbContext(optionsBuilder.Options);
        }
    }
}