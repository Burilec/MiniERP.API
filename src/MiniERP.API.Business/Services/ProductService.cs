using MiniERP.API.Business.Interfaces;
using MiniERP.API.Data.Interfaces;
using MiniERP.API.Models;

namespace MiniERP.API.Business.Services
{
    public sealed class ProductService : IProductService, IAsyncDisposable
    {
        private readonly IProductRepository? _productRepository;

        public ProductService(IProductRepository productRepository)
            => _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

        public async Task AddAsync(Product product)
            => await _productRepository!.AddAsync(product).ConfigureAwait(continueOnCapturedContext: false);

        public async Task UpdateAsync(Product product)
            => await _productRepository!.UpdateAsync(product).ConfigureAwait(continueOnCapturedContext: false);

        public async Task RemoveAsync(Guid apiId) 
            => await _productRepository!.RemoveAsync(apiId).ConfigureAwait(continueOnCapturedContext: false);

        public async Task<Product> GetAsync(Guid apiId)
            => await _productRepository!.FindAsync(_ => _.ApiId == apiId).ConfigureAwait(continueOnCapturedContext: false);

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var allAsync = await _productRepository!.GetAllAsync().ConfigureAwait(continueOnCapturedContext: false);
            return allAsync;
        }

        public async ValueTask DisposeAsync()
        {
            if (_productRepository != null)
                await _productRepository.DisposeAsync();
        }
    }
}