namespace TechStore.Core.Exceptions;

public static class ErrorCodes
{
    //PADRAO
    public const string InternalServerError = "INTERNAL_SERVER_ERROR";

    // USER
    public const string UserLoginInvalidLength = "USER_LOGIN_INVALID_LENGTH";
    public const string UserEmailAlreadyInUse = "USER_EMAIL_ALREADY_IN_USE";
    public const string UserLoginAlreadyInUse = "USER_LOGIN_ALREADY_IN_USE";

    public const string UserLoginRequired = "USER_LOGIN_REQUIRED";

    // CLIENTE
    public const string ClienteNomeRequired = "CLIENTE_NOME_REQUIRED";
    public const string ClienteTelefoneRequired = "CLIENTE_TELEFONE_REQUIRED";
    public const string ClienteNotFound = "CLIENTE_NOT_FOUND";

    // ENDERECO
    public const string EnderecoCepRequired = "ENDERECO_CEP_REQUIRED";
    public const string EnderecoCepInvalid = "ENDERECO_CEP_INVALID";
    public const string EnderecoLogradouroRequired = "ENDERECO_LOGRADOURO_REQUIRED";
    public const string EnderecoNumeroInvalid = "ENDERECO_NUMERO_INVALID";
}
