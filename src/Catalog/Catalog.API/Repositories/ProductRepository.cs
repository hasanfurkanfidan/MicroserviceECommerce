using Catalog.API.Data;
using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;
        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<Product> Create(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> Delete(Product product)
        {
            var deletedEntity =await _catalogContext.Products.DeleteOneAsync(filter: p => p.Id == product.Id);
            return deletedEntity.IsAcknowledged && deletedEntity.DeletedCount > 0;
        }

       

        public async Task<Product> GetProduct(string id)
        {
           return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Category, name);
            return await _catalogContext.Products.Find(filter).ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name,name);
            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _catalogContext.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> Update(Product product)
        {
            var updatedEntity =await _catalogContext.Products.ReplaceOneAsync(filter: p => p.Id == product.Id,replacement:product);
            return updatedEntity.IsAcknowledged && updatedEntity.ModifiedCount>0;

        }
    }
}
