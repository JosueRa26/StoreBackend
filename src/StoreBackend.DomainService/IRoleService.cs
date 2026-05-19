using System;
using StoreBackend.Domain.Entities;

namespace StoreBackend.DomainService;

public interface IRoleService
{
    Task<List<Role>> GetAllAsync();
}