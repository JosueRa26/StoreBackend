using System;
using System.Linq.Expressions;
using System.Net.Mime;
using Microsoft.EntityFrameworkCore.Update;
using StoreBackend.Domain.Entities;
using StoreBackend.DomainService;
using StoreBackend.Dto;
using StoreBackend.Exceptions;
using StoreBackend.Facade.Mappers;
using StoreBackend.Infrastructure;

namespace StoreBackend.Facade;

public class UserFacade : IUserFacade
{
    private readonly IUserService userService;
    private readonly AppDbContext context;    //quien guarda en la base de datos

    public UserFacade(IUserService userService, AppDbContext context)
    {
        this.userService = userService;
        this.context = context;
    }

    public async Task<UserDto> AddAsync(CreateUserDto user)
    {
        var entity  = await userService.AddAsync(user);
        await context.SaveChangesAsync();
        return UserMapper.ToDto(entity);
    }

    public async Task DeleteAsync(Guid ExternalId)
    {
        await userService.DeleteAsync(ExternalId);
        await context.SaveChangesAsync();
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var entities = await userService.GetAllAsync();
        return UserMapper.ToDto(entities);
    }

    public async Task<UserDto> GetByIdAsync(Guid ExternalId)
    {
        var entity = await userService.GetByIdAsync(ExternalId);
        if (entity == null) throw new ResourceNotFoundException();
        return UserMapper.ToDto(entity);
        
    }
}

