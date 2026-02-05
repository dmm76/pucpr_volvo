# TechStore --- Architecture Decision Records (ADR)

## ADR-001 --- Autenticação em memória

**Status:** Ativo (Modo demonstração)

**Contexto:** Necessidade de autenticação simples para facilitar testes
no Swagger e apresentação para banca.

**Decisão:** Utilizar AuthState como Singleton para manter o usuário
logado.

**Consequências:** - Simples de explicar - Rápido para testar - Não
indicado para produção

**Evolução futura:** JWT.

------------------------------------------------------------------------

## ADR-002 --- Domínio rico

**Status:** Ativo

**Decisão:** Entidades com `private set` e métodos de domínio.

**Motivo:** Evitar objetos anêmicos e centralizar regras.

**Consequência:** Código mais previsível e alinhado com práticas
profissionais.

------------------------------------------------------------------------

## ADR-003 --- Repositórios Fake

**Status:** Temporário

**Decisão:** Usar infraestrutura fake para desenvolvimento inicial.

**Motivo:** Foco em arquitetura antes da persistência.

**Evolução futura:** EF Core ou outro ORM.

------------------------------------------------------------------------

## ADR-004 --- Middleware Global de Exceções

**Status:** Ativo

**Decisão:** Converter exceções de domínio em respostas HTTP
padronizadas.

**Benefícios:** - API previsível - Melhor experiência de consumo -
Facilidade de debug

------------------------------------------------------------------------

## ADR-005 --- Guard para rotas Admin

**Status:** Ativo

**Decisão:** Centralizar verificação de login e role em um AdminGuard.

**Motivo:** Evitar repetição de código e garantir segurança consistente.
