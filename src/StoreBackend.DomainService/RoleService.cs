using System;
using StoreBackend.Domain.Entities;
using StoreBackend.Infrastructure.Repositories;

namespace StoreBackend.DomainService;

public class RoleService : IRoleService
{
    private readonly IRoleRepository roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
    }

    public Task<List<Role>> GetAllAsync()
    {
        return roleRepository.GetAllAsync();
    }
}