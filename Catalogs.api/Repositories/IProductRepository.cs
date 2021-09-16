using Catalogs.api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogs.api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
        Task CreateProduct(Product product);
    }
}
