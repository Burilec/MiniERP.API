using DbRewinder;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MiniERP.API.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Xunit;

namespace MiniERP.API.IntegrationTests
{
    [Collection("Oloco")]
    public sealed class BasicTests: IDisposable
    {
        private readonly IntegrationTestsFixture _integrationTestsFixture;
        private readonly IDbRewinderAsyncService? _dbRewinderAsyncService;

        public BasicTests(IntegrationTestsFixture integrationTestsFixture)
        {
            _integrationTestsFixture = integrationTestsFixture;
            _dbRewinderAsyncService = _integrationTestsFixture.ServiceProvider.GetService<IDbRewinderAsyncService>();
        }

        [Fact]
        public async Task PostAndGetProduct()
        {
            var rewindService = _integrationTestsFixture.ServiceProvider.GetService<IDbRewinderAsyncService>();

            await rewindService.InstallAsync().ConfigureAwait(continueOnCapturedContext: false);

            //Arrange
            var product = new Product
            {
                ApiId = Guid.NewGuid(),
                Description = "TestDescription",
                Name = "TestName"
            };

            var productSerialize = JsonConvert.SerializeObject(product);
            var stringContent = new StringContent(productSerialize, Encoding.UTF8);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

            await _integrationTestsFixture.Client.PostAsync("api/Products", stringContent).ConfigureAwait(continueOnCapturedContext: false);

            //Act
            var responseMessage = await _integrationTestsFixture.Client.GetAsync($"api/Products/{product.ApiId}").ConfigureAwait(continueOnCapturedContext: false);

            await rewindService.RewindAsync().ConfigureAwait(continueOnCapturedContext: false);

            //Assert
            responseMessage.IsSuccessStatusCode.Should().BeTrue();

            var responseContent = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);

            var responseProduct = JsonConvert.DeserializeObject<Product>(responseContent);

            responseProduct!.ApiId.Should().Be(product.ApiId);
            responseProduct!.Description.Should().Be(product.Description);
            responseProduct!.Name.Should().Be(product.Name);
        }

        public void Dispose()
        {
            _dbRewinderAsyncService?.UninstallAsync();
        }
    }
}