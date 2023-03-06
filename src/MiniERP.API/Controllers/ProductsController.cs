using Microsoft.AspNetCore.Mvc;
using MiniERP.API.Business.Interfaces;
using MiniERP.API.Models;

namespace MiniERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
            => _productService = productService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productService.GetAllAsync().ConfigureAwait(continueOnCapturedContext: false);
            return new OkObjectResult(products);
        }

        [HttpGet("{apiId:guid}")]
        public async Task<ActionResult<Product>> Get(Guid apiId)
            => new OkObjectResult(await _productService.GetAsync(apiId).ConfigureAwait(continueOnCapturedContext: false));

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            await _productService.AddAsync(product).ConfigureAwait(continueOnCapturedContext: false);

            return Ok();
        }

        [HttpPut("{apiId:guid}")]
        public async Task<ActionResult> Put(Guid apiId, [FromBody] Product product)
        {
            if (apiId != product.ApiId)
            {
                return BadRequest();
            }

            await _productService.UpdateAsync(product).ConfigureAwait(continueOnCapturedContext: false);

            return Ok();
        }

        [HttpDelete("{apiId:guid}")]
        public async Task Delete(Guid apiId)
            => await _productService.RemoveAsync(apiId).ConfigureAwait(continueOnCapturedContext: false);
    }
}
