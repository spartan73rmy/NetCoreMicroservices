using Catalogs.api.Entities;
using MongoDB.Driver;

namespace Catalogs.api.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }


    }
}
