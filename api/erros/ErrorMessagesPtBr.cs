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
            ErrorCodes.UserPasswordHashRequired => "Senha obrigatoria.",
            ErrorCodes.UserLoginRequired => "Login e obrigatorio.",
            ErrorCodes.UserEmailRequired => "Email e obrigatorio.",
            ErrorCodes.UserEmailInvalid => "Email invalido.",

            // CLIENTE
            ErrorCodes.ClienteNomeRequired => "Nome do cliente e obrigatorio.",
            ErrorCodes.ClienteTelefoneRequired => "Telefone do cliente e obrigatorio.",
            ErrorCodes.ClienteNotFound => "Cliente nao encontrado.",
            ErrorCodes.ClienteDefaultShippingAddressNotFound =>
                "Cliente não possui endereço padrão de entrega.",

            // CATEGORY
            ErrorCodes.CategoriaNotFound => "Categoria nao encontrada.",
            ErrorCodes.CategoriaNomeRequired => "Nome da categoria e obrigatorio.",
            ErrorCodes.CategoriaNomeInvalidLength =>
                "Nome da categoria deve ter entre 2 e 80 caracteres.",
            ErrorCodes.CategoriaDescricaoInvalidLength =>
                "Descricao da categoria deve ter no maximo 200 caracteres.",
            ErrorCodes.CategoriaNomeAlreadyExists => "Ja existe uma categoria com esse nome.",

            // ENDERECO
            ErrorCodes.EnderecoCepRequired => "CEP e obrigatorio.",
            ErrorCodes.EnderecoCepInvalid => "CEP invalido.",
            ErrorCodes.EnderecoLogradouroRequired => "Logradouro e obrigatorio.",
            ErrorCodes.EnderecoNumeroInvalid => "Numero do endereco invalido.",

            // PRODUCT
            ErrorCodes.ProductNotFound => "Produto nao encontrado.",
            ErrorCodes.ProductNameRequired => "Nome do produto e obrigatorio.",
            ErrorCodes.ProductNameAlreadyExists => "O nome do produto já está em uso.",
            ErrorCodes.ProductPriceInvalid => "Preco do produto invalido.",
            ErrorCodes.ProductStockInvalid => "Estoque do produto invalido.",
            ErrorCodes.ProductCategoryInvalid => "Categoria do produto invalida.",
            ErrorCodes.ProductInactive => "Produto inativo.",
            ErrorCodes.ProductNameInvalidLength => "Nome precisa ter entre 2 a 120 caracteres.",

            ErrorCodes.ProductDescriptionInvalidLength =>
                "Descrição do produto deve ter no máximo 200 caracteres.",

            // ORDER
            ErrorCodes.OrderNotFound => "Pedido nao encontrado.",
            ErrorCodes.OrderItemsRequired => "Pedido deve ter ao menos um item.",
            ErrorCodes.OrderStatusInvalid =>
                "Operacao nao permitida para o status atual do pedido.",
            ErrorCodes.OrderCustomerRequired => "Cliente e obrigatorio para confirmar o pedido.",
            ErrorCodes.OrderShippingAddressRequired =>
                "Endereco de entrega e obrigatorio para confirmar o pedido.",
            ErrorCodes.OrderPaymentMethodRequired =>
                "Forma de pagamento e obrigatoria para confirmar o pedido.",

            // ORDER ITEM
            ErrorCodes.OrderItemProductRequired => "Produto do item e obrigatorio.",
            ErrorCodes.OrderItemQuantityInvalid => "Quantidade do item invalida.",
            ErrorCodes.OrderItemInsufficientStock => "Estoque insuficiente para o item do pedido.",

            //PERFIL
            ErrorCodes.UserRoleInvalid => "Perfil do usuário informado é inválido.",

            // fallback
            _ => code,
        };
}
