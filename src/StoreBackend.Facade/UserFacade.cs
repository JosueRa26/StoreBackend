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
    private readonly IRoleService roleService;
    private readonly AppDbContext context;    //quien guarda en la base de datos

    public UserFacade(IUserService userService, AppDbContext context, IRoleService roleService)
    {
        this.userService = userService;
        this.context = context;
        this.roleService = roleService;

    }

    public async Task<UserDto> AddAsync(CreateUserDto user)
    {
        var entity = await userService.AddAsync(user);
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
    public async Task<UserRolesDto> GetUserRolesAsync(Guid userId)
    {
        var user = await userService.GetByIdAsync(userId);

        if (user == null)
        {
            throw new ResourceNotFoundException();
        }

        return UserMapper.ToUserRolesDto(user);
    }
    public async Task<UserRolesDto> UpdateUserRolesAsync(Guid userId, UpdateRolesDto dto)
    {
        List<Role>? allRoles = null;

        if (dto.Roles?.Count > 0)
        {
            allRoles = await roleService.GetAllAsync();

            if (dto.Roles.Any(role => !allRoles.Any(e => e.Name.Equals(role))))
            {
                throw new BadRequestResponseException("One or more roles do not exist.");
            }
        }

        var user = await userService.GetByIdAsync(userId);

        if (user == null)
        {
            throw new ResourceNotFoundException();
        }

        user.ClearRoles();

        if (dto.Roles?.Count > 0)
        {
            allRoles ??= await roleService.GetAllAsync();
            var matchedRoles = allRoles.Where(r => dto.Roles.Any(role => r.Name.Equals(role))).ToList();

            var userRoles = matchedRoles.Select(role => new UserRole
            {
                User = user,
                Role = role,
            }).ToList();

            user.UserRoles.AddRange(userRoles);
        }

        await context.SaveChangesAsync();

        return UserMapper.ToUserRolesDto(user);
    }
    public async Task DeleteUserRolesAsync(Guid userId)
    {
        var user = await userService.GetByIdAsync(userId);

        if (user == null)
        {
            throw new ResourceNotFoundException();
        }

        user.ClearRoles();

        await context.SaveChangesAsync();
    }

}

