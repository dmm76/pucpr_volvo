using System.Security.Cryptography;
using TechStore.Core.Dtos;
using TechStore.Core.Entities;
using TechStore.Core.Exceptions;
using TechStore.Core.Interfaces;

namespace TechStore.Core.UseCases.Clientes;

public class ClienteUseCases
{
    private readonly IUserRepository _userRepo;
    private readonly IClienteRepository _clienteRepo;
    private readonly IPasswordHasher _hasher;

    public ClienteUseCases(
        IUserRepository userRepo,
        IClienteRepository clienteRepo,
        IPasswordHasher hasher
    )
    {
        _userRepo = userRepo;
        _clienteRepo = clienteRepo;
        _hasher = hasher;
    }

    public ClienteDetalheDto Cadastrar(
        string nome,
        string telefone,
        string email,
        string login,
        string? senha,
        string? documentoIdentidade,
        Endereco? enderecoOpcional
    )
    {
        // unicidade
        if (_userRepo.ExisteLogin(login))
            throw new BusinessRuleException(ErrorCodes.UserLoginAlreadyInUse);

        if (_userRepo.ExisteEmail(email))
            throw new BusinessRuleException(ErrorCodes.UserEmailAlreadyInUse);

        // senha
        string? senhaTemporaria = null;
        var senhaFinal = senha;

        if (string.IsNullOrWhiteSpace(senhaFinal))
        {
            senhaTemporaria = GerarSenhaForte();
            senhaFinal = senhaTemporaria;
        }

        var user = new User(login: login, email: email, senhaHash: _hasher.Hash(senhaFinal!));

        _userRepo.Inserir(user);

        // garante 1 cliente por user
        if (_clienteRepo.ExistePorUserId(user.Id))
            throw new BusinessRuleException(ErrorCodes.InternalServerError);

        var cliente = new Cliente(
            userId: user.Id,
            nome: nome,
            telefone: telefone,
            documentoIdentidade: documentoIdentidade
        );

        // endereço opcional (já vindo pronto)
        if (enderecoOpcional is not null)
            cliente.AdicionarEndereco(enderecoOpcional);

        _clienteRepo.Inserir(cliente);

        // map
        return Map(cliente, user, senhaTemporaria);
    }

    public ClienteDetalheDto BuscarPorId(int id)
    {
        var c =
            _clienteRepo.BuscarPorId(id) ?? throw new NotFoundException(ErrorCodes.ClienteNotFound);

        var u =
            _userRepo.BuscarPorId(c.UserId)
            ?? throw new NotFoundException(ErrorCodes.InternalServerError);

        return Map(c, u, senhaTemporaria: null);
    }

    public IReadOnlyList<ClienteDetalheDto> BuscarTodos()
    {
        var clientes = _clienteRepo.BuscarTodos();
        return clientes
            .Select(c =>
            {
                var u = _userRepo.BuscarPorId(c.UserId);
                if (u is null)
                    throw new NotFoundException(ErrorCodes.InternalServerError);
                return Map(c, u, null);
            })
            .ToList();
    }

    public ClienteDetalheDto AdicionarEnderecoMe(int userId, CriarEnderecoDto req)
    {
        var cliente =
            _clienteRepo.BuscarPorUserId(userId)
            ?? throw new NotFoundException(ErrorCodes.ClienteNotFound);

        var user =
            _userRepo.BuscarPorId(cliente.UserId)
            ?? throw new NotFoundException(ErrorCodes.InternalServerError);

        var endereco = new Endereco(
            clienteId: cliente.Id,
            descricao: req.Descricao,
            telefone: req.Telefone,
            cep: req.Cep,
            logradouro: req.Logradouro,
            numero: req.Numero,
            complemento: req.Complemento ?? "",
            bairro: req.Bairro,
            cidade: req.Cidade,
            estado: req.Estado,
            pais: req.Pais,
            isDefaultShipping: req.IsDefaultShipping,
            isDefaultBilling: req.IsDefaultBilling
        );

        cliente.AdicionarEndereco(endereco);

        _clienteRepo.Atualizar(cliente);

        return Map(cliente, user, senhaTemporaria: null);
    }

    private static ClienteDetalheDto Map(Cliente c, User u, string? senhaTemporaria) =>
        new(
            c.Id,
            u.Id,
            u.Login,
            u.Email,
            c.Nome,
            c.Telefone,
            c.DocumentoIdentidade,
            c.Enderecos.Select(e => new EnderecoDto(
                    e.Id,
                    e.Descricao,
                    e.Telefone,
                    e.CEP,
                    e.Logradouro,
                    e.Numero,
                    e.Complemento,
                    e.Bairro,
                    e.Cidade,
                    e.Estado,
                    e.Pais,
                    e.IsDefaultShipping,
                    e.IsDefaultBilling
                ))
                .ToList(),
            senhaTemporaria
        );

    private static string GerarSenhaForte()
    {
        // 12 chars, inclui maiúscula/minúscula/número/símbolo
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@#$%&*";
        Span<char> buffer = stackalloc char[12];

        for (int i = 0; i < buffer.Length; i++)
            buffer[i] = chars[RandomNumberGenerator.GetInt32(chars.Length)];

        return new string(buffer);
    }
}
