using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace MiniERP.API.IntegrationTests
{
    public abstract class IntegrationTestsFixture
    {
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            var builder = new WebHostBuilder();

            builder.UseEnvironment("Testing");

            builder.BaseApiConfiguration()

            var host = builder.Build();

            host.Start();

            Client = host.GetTestClient();

            //var clientOptions = new WebApplicationFactoryClientOptions
            //{
            //    AllowAutoRedirect = true,
            //    BaseAddress = new Uri("http://localhost"),
            //    HandleCookies = true,
            //    MaxAutomaticRedirections = 7
            //};

            //Factory = new LojaAppFactory<TStartup>();
            //Client = Factory.CreateClient(clientOptions);
        }
    }
}