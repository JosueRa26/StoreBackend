namespace StoreBackend.Dto;
public class UserDto
{
    public Guid ExternalId {get; set;}
    public string? UserName {get;set;}
    public string? Email {get;set;}
}