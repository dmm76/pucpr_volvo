using TechStore.Core.Exceptions;

namespace TechStore.Api.Errors;

public static class ErrorMessagesPtBr
{
    public static string Get(string code) =>
        code switch
        {
            //PADRAO
            ErrorCodes.InternalServerError => "Erro interno inesperado.",

            // USER
            ErrorCodes.UserLoginInvalidLength => "Login deve ter um tamanho valido.",
            ErrorCodes.UserEmailAlreadyInUse => "E-mail ja esta em uso.",
            ErrorCodes.UserLoginAlreadyInUse => "Login ja esta em uso.",
            ErrorCodes.UserLoginRequired => "Login e obrigatorio.",

            // CLIENTE
            ErrorCodes.ClienteNomeRequired => "Nome do cliente e obrigatorio.",
            ErrorCodes.ClienteTelefoneRequired => "Telefone do cliente e obrigatorio.",
            ErrorCodes.ClienteNotFound => "Cliente nao encontrado.",

            // ENDERECO
            ErrorCodes.EnderecoCepRequired => "CEP e obrigatorio.",
            ErrorCodes.EnderecoCepInvalid => "CEP invalido.",
            ErrorCodes.EnderecoLogradouroRequired => "Logradouro e obrigatorio.",
            ErrorCodes.EnderecoNumeroInvalid => "Numero do endereco invalido.",

            // fallback
            _ => code,
        };
}
