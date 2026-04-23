using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreBackend.Domain.Entities;

namespace StoreBackend.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid ExternalId);
        Task<User> AddAsync(User user);
        Task DeleteAsync(User user);
    }
}