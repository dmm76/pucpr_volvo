using TechStore.Core.Enums;

namespace TechStore.Api.Dtos;

public record AddItemRequest(int ProdutoId, int Quantidade);

public record SetEnderecoRequest(string Endereco);

public record SetPagamentoRequest(FormaPagamento FormaPagamento);

public record IdentificarClienteRequest(int ClienteId, string CustomerNameSnapshot);
