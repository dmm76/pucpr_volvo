namespace TechStore.Api.Dtos;

public record UserLoginRequest(string Login, string Senha);

public record UserLoginResponse(string Message, string Login, string Email, string Role);
