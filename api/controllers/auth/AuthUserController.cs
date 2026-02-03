using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Api.Dtos;
using TechStore.Api.Security;
using TechStore.Infra.Fake.Repositories;

namespace TechStore.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthUserController : ControllerBase
{
    private readonly UserRepositoryFake _repo;
    private readonly AuthState _auth;

    public AuthUserController(UserRepositoryFake repo, AuthState auth)
    {
        _repo = repo;
        _auth = auth;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Login) || string.IsNullOrWhiteSpace(req.Senha))
            return BadRequest(new { message = "Login e senha sao obrigatorios." });

        var user = _repo.BuscarPorLogin(req.Login);
        if (user is null)
            return Unauthorized(new { message = "Login invalido." });

        if (!HashService.Validar(req.Senha, user.SenhaHash))
            return Unauthorized(new { message = "Senha invalida." });

        _auth.Logar(user.Login, user.Email, user.Role);

        return Ok(
            new UserLoginResponse(
                "Usuario autenticado",
                user.Login,
                user.Email,
                user.Role.ToString()
            )
        );
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        _auth.Logout();
        return Ok(new { message = "Usuario deslogado." });
    }

    // opcional: ajuda no Swagger pra ver status
    [HttpGet("status")]
    public IActionResult Status() =>
        Ok(
            new
            {
                userLogado = _auth.UserLogado,
                login = _auth.UserLogin,
                email = _auth.UserEmail,
                role = _auth.UserRole?.ToString(),
            }
        );
}
