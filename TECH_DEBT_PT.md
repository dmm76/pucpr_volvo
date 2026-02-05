# TECH_DEBT.md

## Registro de Débito Técnico

Este documento registra os débitos técnicos conhecidos do projeto
**TechStore**.\
Todos os itens listados aqui são **decisões intencionais**, tomadas para
priorizar a entrega dos fluxos principais de negócio mantendo uma
arquitetura limpa e evolutiva.

------------------------------------------------------------------------

## 1. Testes Automatizados utilizando Repositórios Fake

**Status:** Pendente\
**Prioridade:** Média\
**Nível de Risco:** Baixo (contexto acadêmico atual)

### Contexto

Os repositórios fake foram implementados para simular a persistência e
permitir a execução rápida e isolada da lógica de negócio sem depender
de banco de dados.

Embora a arquitetura já esteja preparada para testes, a implementação
dos testes automatizados foi adiada para priorizar os fluxos centrais do
domínio.

### Por que esta decisão é aceitável

-   A arquitetura já é testável.
-   A infraestrutura está devidamente isolada do Core.
-   Os repositórios fake permitem comportamento determinístico.
-   Nenhum compromisso estrutural foi introduzido.

### Impacto

O impacto atual é mínimo porque:

-   O projeto está em fase acadêmica / demonstrativa.
-   As regras de negócio permanecem centralizadas no domínio.
-   Os repositórios podem ser substituídos sem afetar a camada de
    aplicação.

### Plano Futuro

Implementar testes automatizados com foco no comportamento do negócio:

-   Testes unitários para **ProdutoUseCases**
-   Testes unitários para **PedidoUseCases**
-   Validação das invariantes do domínio
-   Validação dos fluxos de erro
-   Cobertura de cenários com repositórios fake

------------------------------------------------------------------------

## Princípio Norteador

> **"Prefira um débito técnico consciente a uma arquitetura
> acidental."**

Débito técnico é aceitável quando:

-   Está documentado\
-   É intencional\
-   Não compromete o design do sistema\
-   Existe um caminho claro para resolução

------------------------------------------------------------------------

## Observação Arquitetural

A arquitetura atual segue os seguintes princípios:

-   Domínio protegido de preocupações de infraestrutura\
-   Camada de persistência substituível\
-   Infraestrutura fake utilizada estritamente para testes e
    desenvolvimento\
-   Nenhuma adaptação do domínio para suportar implementações fake

Isso garante que melhorias futuras --- incluindo testes automatizados
--- possam ser adicionadas com segurança, sem necessidade de
refatorações estruturais.

------------------------------------------------------------------------

**Última atualização:** 2026-02-05
