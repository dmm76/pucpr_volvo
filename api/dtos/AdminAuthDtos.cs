namespace TechStore.Api.Dtos;

public record AdminLoginRequest(string Login, string Senha);

public record AdminLoginResponse(string Message, string Login, string Email);
