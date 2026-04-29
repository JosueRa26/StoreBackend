using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreBackend.Domain.Entities;

namespace StoreBackend.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid ExternalId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.ExternalId == ExternalId);
        }

        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<bool> HasUserByUsernameAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }
           public async Task<bool> HasUserByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.UserName == email);
        }
        

    
    }
}