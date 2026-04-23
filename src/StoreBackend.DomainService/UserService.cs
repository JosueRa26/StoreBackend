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

        public Task<User> AddAsync(UserDto user)
        {
            var userEntity = new User
            {
                ExternalId = user.ExternalId,
                UserName = user.UserName,   
                Email = user.Email
            };

            return _userRepository.AddAsync(userEntity);
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