using TechStore.Core.Exceptions;

namespace TechStore.Core.Entities;

public class Categoria
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = "";
    public string? Descricao { get; private set; }

    public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; private set; }

    protected Categoria() { }

    public Categoria(string nome, string? descricao = null)
    {
        AtualizarNome(nome);
        AtualizarDescricao(descricao);
    }

    public void AtualizarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new BusinessRuleException(ErrorCodes.CategoriaNomeRequired);

        var n = nome.Trim();

        if (n.Length < 2 || n.Length > 80)
            throw new BusinessRuleException(ErrorCodes.CategoriaNomeInvalidLength);

        if (Nome == n)
            return;

        Nome = n;
        MarcarAtualizacao();
    }

    public void AtualizarDescricao(string? descricao)
    {
        var d = string.IsNullOrWhiteSpace(descricao) ? null : descricao.Trim();

        if (d is not null && d.Length > 200)
            throw new BusinessRuleException(ErrorCodes.CategoriaDescricaoInvalidLength);

        if (Descricao == d)
            return;

        Descricao = d;
        MarcarAtualizacao();
    }

    private void MarcarAtualizacao() => DataAtualizacao = DateTime.UtcNow;
}
