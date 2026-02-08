using TechStore.Core.Dtos;
using TechStore.Core.Entities;
using TechStore.Core.Exceptions;
using TechStore.Core.Interfaces;

namespace TechStore.Core.useCases.categorias;

public class CategoriaUseCases
{
    private readonly ICategoriaRepository _repo;

    public CategoriaUseCases(ICategoriaRepository repo)
    {
        _repo = repo;
    }

    public List<CategoriaDto> Listar() =>
        _repo.BuscarTodos().Select(c => new CategoriaDto(c.Id, c.Nome, c.Descricao)).ToList();

    public CategoriaDto BuscarPorId(int id)
    {
        var c = _repo.BuscarPorId(id);
        if (c is null)
            throw new NotFoundException(ErrorCodes.CategoriaNotFound);

        return new CategoriaDto(c.Id, c.Nome, c.Descricao);
    }

    public CategoriaDto Criar(string nome, string? descricao = null)
    {
        if (_repo.ExisteNome(nome))
            throw new BusinessRuleException(ErrorCodes.CategoriaNomeAlreadyExists);

        var criada = _repo.Inserir(new Categoria(nome, descricao));
        return new CategoriaDto(criada.Id, criada.Nome, criada.Descricao);
    }

    public CategoriaDto Atualizar(int id, string? nome, string? descricao)
    {
        var c = _repo.BuscarPorId(id);
        if (c is null)
            throw new NotFoundException(ErrorCodes.CategoriaNotFound);

        // Nome: null = mantém
        if (nome is not null)
        {
            var nomeTrim = nome.Trim();

            // só checa duplicidade se realmente mudou
            if (!string.Equals(c.Nome, nomeTrim, StringComparison.Ordinal))
            {
                if (_repo.ExisteNome(nomeTrim))
                    throw new BusinessRuleException(ErrorCodes.CategoriaNomeAlreadyExists);
            }

            c.AtualizarNome(nomeTrim); // valida vazio/tamanho aqui
        }

        // Descricao: null = mantém
        if (descricao is not null)
        {
            c.AtualizarDescricao(descricao); // sua entity já faz trim e valida tamanho
        }

        _repo.Atualizar(c);

        return new CategoriaDto(c.Id, c.Nome, c.Descricao);
    }

    public void Remover(int id)
    {
        var c = _repo.BuscarPorId(id);
        if (c is null)
            throw new NotFoundException(ErrorCodes.CategoriaNotFound);

        _repo.Remover(id);
    }
}
