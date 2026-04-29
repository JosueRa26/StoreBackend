using System;
using System.Collections.Generic; // Necesario para usar List<>
using StoreBackend.Dto;

namespace StoreBackend.Facade;

public interface IUserFacade
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(Guid ExternalId);
    Task<UserDto> AddAsync(CreateUserDto user);
    Task DeleteAsync(Guid ExternalId);
}