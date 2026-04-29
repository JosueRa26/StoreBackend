using System;
using System.Linq; // Necesario para .Select() y .ToList()
using System.Collections.Generic; // Necesario para List<>
using StoreBackend.Api.Models.Requests;
using StoreBackend.Api.Models.Responses;
using StoreBackend.Dto;

namespace StoreBackend.Api.Mappers;

public class UserMapper
{
    // public static UserDto ToDto(CreateUserRequestModel model)
    // {
    //     return new UserDto
    //     {
    //         UserName = model.UserName,
    //         Email = model.Email
    //     };
    // }

    public static List<UserResponseModel> ToModel(List<UserDto> users)
    {
        return users.Select(u => ToModel(u)).ToList();
    }

    public static UserResponseModel ToModel(UserDto user)
    {
        return new UserResponseModel
        {
            ExternalId = user.ExternalId,
            UserName = user.UserName,
            Email = user.Email
        };
    }
    
    public static CreateUserDto ToDto(CreateUserRequestModel user)
    {
        return new CreateUserDto
        {
            Username = user.UserName,
            Email = user.Email,
            Password = user.Password,
        };
    }
    
}