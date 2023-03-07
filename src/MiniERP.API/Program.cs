using Microsoft.AspNetCore;
using MiniERP.API.Configuration;

namespace MiniERP.API
{
    public static class Program
    {
        public static void Main(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                      .ConfigureAppConfiguration(ConfigureAppConfiguration())
                      .BaseApiConfiguration()
                      .Build()
                      .Run();

        private static Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureAppConfiguration()
            => (context, configuration) => configuration.SetBasePath(context.HostingEnvironment.ContentRootPath)
                                                        .AddJsonFile("appsettings.json", true, true)
                                                        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true)
                                                        .AddEnvironmentVariables();
    }
}