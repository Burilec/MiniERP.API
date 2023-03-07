using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MiniERP.API.Configuration;
using Xunit;

namespace MiniERP.API.IntegrationTests
{
    [CollectionDefinition("Oloco")]
    public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture>
    {
    }

    public sealed class IntegrationTestsFixture
    {
        private const string HostEnvironment = "Local";

        public readonly HttpClient Client;

        public IServiceProvider ServiceProvider { get; internal set; }



        public IntegrationTestsFixture()
        {
            var hostBuilder = new HostBuilder().ConfigureDefaults(Array.Empty<string>());

            hostBuilder.UseEnvironment(HostEnvironment)
                       .ConfigureHostConfiguration(_ => _.SetBasePath(Environment.CurrentDirectory)
                                                         .AddJsonFile($"appsettings.{HostEnvironment}.json", true, true))
                       .ConfigureWebHostDefaults(_ => _.UseTestServer()
                                                       .BaseApiConfiguration());

            var host = hostBuilder.Build();
            host.Start();
            var testServer = host.GetTestServer();

            Client = testServer.CreateClient();
            ServiceProvider = host.Services;
        }
    }
}