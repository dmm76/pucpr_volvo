# TechStore --- Visão Arquitetural

## 🎯 Objetivo do Sistema

API backend de um ecommerce de informática, construída seguindo
princípios de arquitetura limpa, separação de responsabilidades e
domínio rico.

------------------------------------------------------------------------

## 🧠 Decisões Arquiteturais

-   Camadas inspiradas em Clean Architecture
-   Domínio como centro do sistema
-   Controllers finos
-   Regras dentro das Entities

------------------------------------------------------------------------

## ✔️ Autenticação (Modo Demonstração)

**Estratégia atual:** - AuthState Singleton mantém usuário logado em
memória. - AdminGuard protege rotas administrativas.

**Motivo:** simplificação para demonstração e banca.\
**Evolução futura:** JWT / autenticação stateless.

------------------------------------------------------------------------

## 🧱 Estrutura do Projeto

### Core (Coração do sistema)

**Entities (Domínio Rico):** - Categoria --- validações e métodos de
domínio - Produto - Cliente - Pedido - ItemPedido - User

Todas seguem: \> estado protegido + comportamento

------------------------------------------------------------------------

## Exceptions

-   BusinessRuleException → violações de regra
-   NotFoundException → recurso inexistente

------------------------------------------------------------------------

## Middleware Global

### ExceptionMiddleware

Converte exceções em respostas HTTP padronizadas.

  Exception      HTTP
  -------------- ------
  BusinessRule   400
  NotFound       404
  Unexpected     500

------------------------------------------------------------------------

## Infra (Fake)

Repositórios fake permitem desenvolvimento sem banco real.

### FakeEntitySetter

Permite setar propriedades privadas via reflection mantendo o domínio
protegido.

------------------------------------------------------------------------

## UseCases

Controllers apenas orquestram --- regras vivem no domínio.

### CategoriaUseCases

-   Listar
-   BuscarPorId
-   Criar

------------------------------------------------------------------------

## Segurança

### AdminGuard

Bloqueia quando: - não logado - não admin

Retornos: - 401 --- não autenticado - 403 --- sem permissão

------------------------------------------------------------------------

## 📊 Estado Atual do Sistema

### ✔️ Implementado

-   Auth
-   Guard
-   Exception Middleware
-   Categoria (domínio completo)
-   Seed fake
-   Swagger
-   UseCases

### 🚧 Próximo módulo

👉 Produto --- agregado central do ecommerce.

------------------------------------------------------------------------

## 🧭 Filosofia do Projeto

> "Comece simples. Proteja o que é crítico."

Evita: - estado público - regra espalhada - objetos anêmicos

------------------------------------------------------------------------

## 💡 Evoluções Planejadas

-   Produto
-   Carrinho / Pedido
-   Persistência real
-   JWT
-   Logs estruturados
-   Cache

------------------------------------------------------------------------

## 🧠 Autor

Douglas Marcelo Monquero\
Engenharia de Software
