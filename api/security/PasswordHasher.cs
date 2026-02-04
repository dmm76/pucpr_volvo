using System.Security.Cryptography;
using System.Text;
using TechStore.Core.Interfaces;

namespace TechStore.Api.Security;

public class PasswordHasher : IPasswordHasher
{
    private const string SALT = "TechStore#2026!";

    public string Hash(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
            throw new ArgumentException("Senha obrigatoria.");

        string texto = SALT + senha;

        using var sha = SHA256.Create();
        byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(texto));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }

    public bool Verify(string senhaDigitada, string hashSalvo)
    {
        if (string.IsNullOrWhiteSpace(hashSalvo))
            return false;
        return Hash(senhaDigitada) == hashSalvo;
    }
}
