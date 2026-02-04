namespace TechStore.Core.Exceptions;

public sealed class BusinessRuleException : DomainException
{
    public BusinessRuleException(string code, string? message = null)
        : base(code, message) { }
}
