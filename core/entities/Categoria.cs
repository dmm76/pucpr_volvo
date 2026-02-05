using TechStore.Core.Exceptions;

namespace TechStore.Core.Entities;

public class Categoria
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = "";
    public string? Descricao { get; private set; }

    public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; private set; } = DateTime.UtcNow;

    protected Categoria() { }

    public Categoria(string nome, string? descricao = null)
    {
        AtualizarNome(nome);
        AtualizarDescricao(descricao);
    }

    public void AtualizarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new BusinessRuleException("Nome da categoria é obrigatório.");

        var n = nome.Trim();

        if (n.Length < 2 || n.Length > 80)
            throw new BusinessRuleException("Nome da categoria deve ter entre 2 e 80 caracteres.");

        if (Nome == n)
            return;

        Nome = n;
        MarcarAtualizacao();
    }

    public void AtualizarDescricao(string? descricao)
    {
        var d = string.IsNullOrWhiteSpace(descricao) ? null : descricao.Trim();

        if (d is not null && d.Length > 200)
            throw new BusinessRuleException(
                "Descrição da categoria deve ter no máximo 200 caracteres."
            );

        if (Descricao == d)
            return;

        Descricao = d;
        MarcarAtualizacao();
    }

    private void MarcarAtualizacao() => DataAtualizacao = DateTime.UtcNow;
}
