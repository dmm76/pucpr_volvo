using System.Security.Cryptography;
using System.Text;

namespace TechStore.Api.Security;

public static class HashService
{
    private const string SALT = "TechStore#2026!";

    public static string GerarHash(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
            throw new ArgumentException("Senha é obrigatória.");

        string texto = SALT + senha;

        using var sha = SHA256.Create();
        byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(texto));

        return Convert.ToHexString(bytes).ToLowerInvariant();
    }

    public static bool Validar(string senhaDigitada, string hashSalvo)
    {
        if (string.IsNullOrWhiteSpace(hashSalvo))
            return false;
        return GerarHash(senhaDigitada) == hashSalvo;
    }
}
