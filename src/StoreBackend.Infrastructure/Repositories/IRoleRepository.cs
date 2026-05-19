using System;
using StoreBackend.Domain.Entities;

namespace StoreBackend.Infrastructure.Repositories;

public interface IRoleRepository
{
    Task<List<Role>> GetAllAsync();
}