using FluentAssertions;
using MiniERP.API.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Xunit;

namespace MiniERP.API.IntegrationTests
{
    [Collection("Oloco")]
    public class BasicTests
    {
        private readonly IntegrationTestsFixture _integrationTestsFixture;

        public BasicTests(IntegrationTestsFixture integrationTestsFixture)
            => _integrationTestsFixture = integrationTestsFixture;

        [Fact]
        public async Task TestBasic()
        {
            //Arrange
            var product = new Product
            {
                ApiId = Guid.NewGuid(),
                Description = "TestDescription",
                Name = "TestName"
            };

            var productSerialize = JsonConvert.SerializeObject(product);
            var stringContent = new StringContent(productSerialize,Encoding.UTF8);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

            await _integrationTestsFixture.Client.PostAsync($"api/Products", stringContent).ConfigureAwait(continueOnCapturedContext: false);

            //Act
            var responseMessage = await _integrationTestsFixture.Client.GetAsync($"api/Products/{product.ApiId}").ConfigureAwait(continueOnCapturedContext: false);

            //Assert
            responseMessage.IsSuccessStatusCode.Should().BeTrue();

            var responseContent = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);

            var responseProduct = JsonConvert.DeserializeObject<Product>(responseContent);

            responseProduct!.ApiId.Should().Be(product.ApiId);
            responseProduct!.Description.Should().Be(product.Description);
            responseProduct!.Name.Should().Be(product.Name);
        }
    }
}