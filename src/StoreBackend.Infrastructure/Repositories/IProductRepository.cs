using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreBackend.Domain.Entities;

namespace StoreBackend.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid productId);
        Task<Product> AddAsync(Product product);
        Task DeleteAsync(Product product);
    }
}