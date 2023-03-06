using MiniERP.API.Data.Interfaces;
using MiniERP.API.Models;

namespace MiniERP.API.Data.Repositories
{
    public sealed class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(MiniERPDbContext db) : base(db)
        {
        }
    }
}