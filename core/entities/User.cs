using TechStore.Core.Enums;
using TechStore.Core.Exceptions;

namespace TechStore.Core.Entities;

public class User
{
    public int Id { get; private set; }
    public string Login { get; private set; } = "";
    public string Email { get; private set; } = "";
    public string SenhaHash { get; private set; } = "";
    public UserRole Role { get; private set; } = UserRole.Usuario;

    public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; private set; } = DateTime.UtcNow;

    protected User() { }

    public User(string login, string email, string senhaHash, UserRole role = UserRole.Usuario)
    {
        AtualizarLogin(login);
        AtualizarEmail(email);
        AlterarSenhaHash(senhaHash);
        DefinirRole(role);
    }

    public void MarcarAtualizacao() => DataAtualizacao = DateTime.UtcNow;

    public void AtualizarLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new BusinessRuleException(ErrorCodes.UserLoginRequired);

        var novo = login.Trim().ToLowerInvariant();

        // se você quiser usar o code de tamanho, aplique aqui:
        if (novo.Length < 3 || novo.Length > 30)
            throw new BusinessRuleException(ErrorCodes.UserLoginInvalidLength);

        if (Login == novo)
            return;

        Login = novo;
        MarcarAtualizacao();
    }

    public void AtualizarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new BusinessRuleException(ErrorCodes.UserEmailRequired);

        var novo = email.Trim().ToLowerInvariant();

        if (novo.Length < 5 || !novo.Contains('@'))
            throw new BusinessRuleException(ErrorCodes.UserEmailInvalid);

        if (Email == novo)
            return;

        Email = novo;
        MarcarAtualizacao();
    }

    public void AlterarSenhaHash(string senhaHash)
    {
        if (string.IsNullOrWhiteSpace(senhaHash))
            throw new BusinessRuleException(ErrorCodes.UserPasswordHashRequired);

        var novo = senhaHash;

        if (SenhaHash == novo)
            return;

        SenhaHash = novo;
        MarcarAtualizacao();
    }

    public void DefinirRole(UserRole role)
    {
        if (!Enum.IsDefined(typeof(UserRole), role))
            throw new BusinessRuleException(ErrorCodes.UserRoleInvalid);

        if (Role == role)
            return;

        Role = role;
        MarcarAtualizacao();
    }
}
