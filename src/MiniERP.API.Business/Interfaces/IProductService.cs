using MiniERP.API.Models;

namespace MiniERP.API.Business.Interfaces
{
    public interface IProductService
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(Guid apiId);
        Task<Product> GetAsync(Guid apiId);
        Task<IEnumerable<Product>> GetAllAsync();
    }
}