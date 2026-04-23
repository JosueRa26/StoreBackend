using System;

namespace StoreBackend.Api.Models.Responses;

public class UserResponseModel
{
    public Guid ExternalId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
}