using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreBackend.Domain.Entities;
using StoreBackend.Dto;
using StoreBackend.Exceptions;
using StoreBackend.Infrastructure.Repositories;

namespace StoreBackend.DomainService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<List<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<User?> GetByIdAsync(Guid ExternalId)
        {
            return _userRepository.GetByIdAsync(ExternalId);
        }

        public async Task<User> AddAsync(CreateUserDto user)
        {
            if(await _userRepository.HasUserByUsernameAsync(user.Username))
            {
                throw new Exceptions.BadRequestResponseException("Username is already taken");
            }
            if (await _userRepository.HasUserByEmailAsync(user.Email))
            {
                throw new Exceptions.BadRequestResponseException("Email is already taken");
            }
            var entity = new User
            {
                ExternalId = Guid.NewGuid(),
                UserName = user.Username,
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password) //encriptar la contraseña
            };
            return await _userRepository.AddAsync(entity);
            
        }

        public async Task DeleteAsync(Guid ExternalId)
        {
            var user = await _userRepository.GetByIdAsync(ExternalId);

            if (user == null)
                throw new ResourceNotFoundException();

            await _userRepository.DeleteAsync(user);
        }
    }
}