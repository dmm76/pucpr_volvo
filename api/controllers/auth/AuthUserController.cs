using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Api.Dtos.Users;
using TechStore.Core.Interfaces;

namespace TechStore.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthUserController : ControllerBase
{
    private readonly IUserRepository _repo;
    private readonly IClienteRepository _clienteRepo;
    private readonly AuthState _auth;
    private readonly IPasswordHasher _hasher;

    public AuthUserController(
        IUserRepository repo,
        IClienteRepository clienteRepo,
        AuthState auth,
        IPasswordHasher hasher
    )
    {
        _repo = repo;
        _clienteRepo = clienteRepo;
        _auth = auth;
        _hasher = hasher;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Login) || string.IsNullOrWhiteSpace(req.Senha))
            return BadRequest(new { message = "Login e senha sao obrigatorios." });

        var user = _repo.BuscarPorLogin(req.Login);
        if (user is null)
            return Unauthorized(new { message = "Login invalido." });

        if (!_hasher.Verify(req.Senha, user.SenhaHash))
            return Unauthorized(new { message = "Senha invalida." });

        var cliente = _clienteRepo.BuscarPorUserId(user.Id);

        _auth.Logar(user.Id, user.Login, user.Email, user.Role, cliente?.Id);

        return Ok(
            new
            {
                message = "Usuario autenticado",
                userId = user.Id,
                clienteId = cliente?.Id,
                login = user.Login,
                email = user.Email,
                role = user.Role.ToString(),
            }
        );
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        _auth.Logout();
        return Ok(new { message = "Usuario deslogado." });
    }

    [HttpGet("status")]
    public IActionResult Status() =>
        Ok(
            new
            {
                userLogado = _auth.UserLogado,
                userId = _auth.UserId,
                clienteId = _auth.ClienteId,
                login = _auth.UserLogin,
                email = _auth.UserEmail,
                role = _auth.UserRole?.ToString(),
            }
        );
}
