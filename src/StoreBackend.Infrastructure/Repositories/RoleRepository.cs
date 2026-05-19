using System;
using Microsoft.EntityFrameworkCore;
using StoreBackend.Domain.Entities;

namespace StoreBackend.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Role>> GetAllAsync()
    {
        return _context.Roles.ToListAsync();
    }
}