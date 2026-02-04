namespace TechStore.Core.Exceptions;

public sealed class NotFoundException : DomainException
{
    public NotFoundException(string code, string? message = null)
        : base(code, message) { }
}
