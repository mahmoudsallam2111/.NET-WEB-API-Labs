namespace API_Identity.Dtos;

    public record UserDto (string UserName , string Password);
    public record TokenDto (string Token , DateTime Expire);

public record RegisterDto(string UserName, string Password , string Email , string ClassLevel);
