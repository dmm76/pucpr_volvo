namespace TechStore.Core.Exceptions;

public abstract class DomainException : Exception
{
    public string Code { get; }

    protected DomainException(string code, string? message = null)
        : base(message ?? code)
    {
        Code = code;
    }
}
