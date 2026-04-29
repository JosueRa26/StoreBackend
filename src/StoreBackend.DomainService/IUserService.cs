using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreBackend.Domain.Entities;
using StoreBackend.Dto;

namespace StoreBackend.DomainService
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid ExternalId);
        Task<User> AddAsync(CreateUserDto user);
        Task DeleteAsync(Guid ExternalId);
    }
}