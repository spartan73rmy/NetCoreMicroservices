using Catalogs.api.Data;
using Catalogs.api.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogs.api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
            => this.catalogContext = catalogContext;

        public async Task CreateProduct(Product product)
            => await catalogContext.Products.InsertOneAsync(product);

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleted = await catalogContext.Products.DeleteOneAsync(filter);

            return deleted.IsAcknowledged && deleted.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
            => await catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetProducts()
            => await catalogContext.Products.Find(p => true).ToListAsync();
        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await catalogContext.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
