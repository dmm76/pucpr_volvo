namespace TechStore.Core.Exceptions;

public static class ErrorCodes
{
    //PADRAO
    public const string InternalServerError = "INTERNAL_SERVER_ERROR";

    // USER
    public const string UserLoginInvalidLength = "USER_LOGIN_INVALID_LENGTH";
    public const string UserEmailAlreadyInUse = "USER_EMAIL_ALREADY_IN_USE";
    public const string UserLoginAlreadyInUse = "USER_LOGIN_ALREADY_IN_USE";
    public const string UserPasswordHashRequired = "USER_PASSWORD_HASH_REQUIRED";

    public const string UserLoginRequired = "USER_LOGIN_REQUIRED";
    public const string UserEmailRequired = "USER_EMAIL_REQUIRED";

    public const string UserEmailInvalid = "USER_EMAIL_INVALID";

    // CLIENTE
    public const string ClienteNomeRequired = "CLIENTE_NOME_REQUIRED";
    public const string ClienteTelefoneRequired = "CLIENTE_TELEFONE_REQUIRED";
    public const string ClienteNotFound = "CLIENTE_NOT_FOUND";

    public const string ClienteDefaultShippingAddressNotFound =
        "CLIENT_DEFAULT_SHIPPING_ADDRESS_NOT_FOUND";

    /// CATEGORY
    public const string CategoriaNotFound = "CATEGORY_NOT_FOUND";
    public const string CategoriaNomeRequired = "CATEGORY_NAME_REQUIRED";
    public const string CategoriaNomeInvalidLength = "CATEGORY_NAME_INVALID_LENGTH";
    public const string CategoriaDescricaoInvalidLength = "CATEGORY_DESCRIPTION_INVALID_LENGTH";
    public const string CategoriaNomeAlreadyExists = "CATEGORY_NAME_ALREADY_EXISTS";

    // ENDERECO
    public const string EnderecoCepRequired = "ENDERECO_CEP_REQUIRED";
    public const string EnderecoCepInvalid = "ENDERECO_CEP_INVALID";
    public const string EnderecoLogradouroRequired = "ENDERECO_LOGRADOURO_REQUIRED";
    public const string EnderecoNumeroInvalid = "ENDERECO_NUMERO_INVALID";

    /// PRODUCT
    public const string ProductNotFound = "PRODUCT_NOT_FOUND";
    public const string ProductNameRequired = "PRODUCT_NAME_REQUIRED";

    public const string ProductNameAlreadyExists = "PRODUCT_NAME_ALREADY_EXISTS";
    public const string ProductPriceInvalid = "PRODUCT_PRICE_INVALID";
    public const string ProductStockInvalid = "PRODUCT_STOCK_INVALID";
    public const string ProductCategoryInvalid = "PRODUCT_CATEGORY_INVALID";
    public const string ProductInactive = "PRODUCT_INACTIVE";
    public const string ProductNameInvalidLength = "PRODUCT_NAME_INVALID_LENGTH";

    // ORDER (Pedido)
    public const string OrderNotFound = "ORDER_NOT_FOUND";
    public const string OrderItemsRequired = "ORDER_ITEMS_REQUIRED";
    public const string OrderStatusInvalid = "ORDER_STATUS_INVALID";
    public const string OrderCustomerRequired = "ORDER_CUSTOMER_REQUIRED";
    public const string OrderShippingAddressRequired = "ORDER_SHIPPING_ADDRESS_REQUIRED";
    public const string OrderPaymentMethodRequired = "ORDER_PAYMENT_METHOD_REQUIRED";

    // ORDER ITEM (ItemPedido)
    public const string OrderItemProductRequired = "ORDER_ITEM_PRODUCT_REQUIRED";
    public const string OrderItemQuantityInvalid = "ORDER_ITEM_QUANTITY_INVALID";
    public const string OrderItemInsufficientStock = "ORDER_ITEM_INSUFFICIENT_STOCK";

    //ROLE
    public const string UserRoleInvalid = "USER_ROLE_INVALID";
}
