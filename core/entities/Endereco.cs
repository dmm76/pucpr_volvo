namespace TechStore.Core.Entities;

public class Endereco
{
    public int Id { get; private set; }

    public int ClienteId { get; private set; }
    public Cliente Cliente { get; private set; } = null!;

    public string Descricao { get; private set; } = "";

    public string Telefone { get; private set; } = "";

    public string CEP { get; private set; } = "";

    public int CodIbge { get; private set; }

    public double Latitude { get; private set; }

    public double Longitude { get; private set; }

    public string Logradouro { get; private set; } = "";

    public int Numero { get; private set; }

    public string Complemento { get; private set; } = "";

    public string Bairro { get; private set; } = "";
    public string Cidade { get; private set; } = "";
    public string Estado { get; private set; } = "";
    public string Pais { get; private set; } = "";

    public bool IsDefaultShipping { get; private set; }

    public bool IsDefaultBilling { get; private set; }

    public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; private set; } = DateTime.UtcNow;

    protected Endereco() { }

    public Endereco(
        int clienteId,
        string descricao,
        string telefone,
        string cep,
        int codIbge,
        double latitude,
        double longitude,
        string logradouro,
        int numero,
        string complemento,
        string bairro,
        string cidade,
        string estado,
        string pais,
        bool isDefaultShipping,
        bool isDefaultBilling
    )
    {
        ClienteId = clienteId;
        Descricao = descricao;
        Telefone = telefone;
        CEP = cep;
        CodIbge = codIbge;
        Latitude = latitude;
        Longitude = longitude;
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        Pais = pais;
        IsDefaultShipping = isDefaultShipping;
        IsDefaultBilling = isDefaultBilling;
    }

    public void DefinirDefaultShipping(bool valor)
    {
        IsDefaultShipping = valor;
        DataAtualizacao = DateTime.UtcNow;
    }

    public void DefinirDefaultBilling(bool valor)
    {
        IsDefaultBilling = valor;
        DataAtualizacao = DateTime.UtcNow;
    }
}
