using Microsoft.AspNetCore.Mvc;
using TechStore.Api.Auth;
using TechStore.Api.Dtos;
using TechStore.Api.Security;
using TechStore.Infra.Fake.Repositories;

namespace TechStore.Api.Controllers.Auth;

[ApiController]
[Route("api/admin/auth")]
public class AuthAdminController : ControllerBase
{
    private readonly AdminRepositoryFake _repo;
    private readonly AuthState _auth;

    public AuthAdminController(AdminRepositoryFake repo, AuthState auth)
    {
        _repo = repo;
        _auth = auth;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] AdminLoginRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Login) || string.IsNullOrWhiteSpace(req.Senha))
            return BadRequest(new { message = "Login e senha são obrigatórios." });

        var admin = _repo.BuscarPorLogin(req.Login);
        if (admin is null)
            return Unauthorized(new { message = "Login inválido." });

        if (!HashService.Validar(req.Senha, admin.SenhaHash))
            return Unauthorized(new { message = "Senha inválida." });

        _auth.Logar(admin.Login, admin.Email);

        return Ok(new AdminLoginResponse("Admin autenticado", admin.Login, admin.Email));
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        _auth.Logout();
        return Ok(new { message = "Admin deslogado." });
    }

    // opcional: ajuda no Swagger pra ver status
    [HttpGet("status")]
    public IActionResult Status() =>
        Ok(
            new
            {
                adminLogado = _auth.AdminLogado,
                login = _auth.AdminLogin,
                email = _auth.AdminEmail,
            }
        );
}
