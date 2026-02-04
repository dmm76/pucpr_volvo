using TechStore.Core.Exceptions;

namespace TechStore.Core.Entities;

public class Cliente
{
    public int Id { get; private set; }

    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    public string Nome { get; private set; } = "";
    public string Telefone { get; private set; } = "";

    private readonly List<Endereco> _enderecos = new();
    public IReadOnlyCollection<Endereco> Enderecos => _enderecos;

    public string? DocumentoIdentidade { get; private set; }

    public DateTime? EmailVerificadoEm { get; private set; }

    public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; private set; } = DateTime.UtcNow;

    protected Cliente() { }

    public Cliente(int userId, string nome, string telefone, string? documentoIdentidade = null)
    {
        UserId = userId;
        AtualizarNome(nome);
        AtualizarTelefone(telefone);
        DocumentoIdentidade = documentoIdentidade;
    }

    public void AtualizarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new BusinessRuleException(ErrorCodes.ClienteNomeRequired);

        var novo = nome.Trim();
        if (Nome == novo)
            return;

        Nome = novo;
        MarcarAtualizacao();
    }

    public void AtualizarTelefone(string telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
            throw new BusinessRuleException(ErrorCodes.ClienteTelefoneRequired);

        var novo = telefone.Trim();
        if (Telefone == novo)
            return;

        Telefone = novo;
        MarcarAtualizacao();
    }

    public void MarcarAtualizacao() => DataAtualizacao = DateTime.UtcNow;

    // opcionais: métodos de domínio
    public void VerificarEmail()
    {
        EmailVerificadoEm = DateTime.UtcNow;
        MarcarAtualizacao();
    }

    public void AdicionarEndereco(Endereco endereco)
    {
        if (endereco == null)
            return;

        if (endereco.IsDefaultShipping)
        {
            foreach (var e in _enderecos)
                e.DefinirDefaultShipping(false);
        }

        if (endereco.IsDefaultBilling)
        {
            foreach (var e in _enderecos)
                e.DefinirDefaultBilling(false);
        }

        _enderecos.Add(endereco);
        MarcarAtualizacao();
    }

    public void RemoverEndereco(Endereco endereco)
    {
        if (endereco == null)
            return;
        if (_enderecos.Remove(endereco))
        {
            MarcarAtualizacao();
        }
    }
}
