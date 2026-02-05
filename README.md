# 🖥️ TechStore API

### Backend arquitetado para um ecommerce moderno de informática

> "Comece simples. Proteja o que é crítico."

------------------------------------------------------------------------

## 🚀 Sobre o Projeto

O **TechStore** é uma Web API desenvolvida em **.NET**, projetada desde
o início com **mentalidade arquitetural**, priorizando:

-   Domínio rico\
-   Separação de responsabilidades\
-   Previsibilidade\
-   Código sustentável\
-   Facilidade de evolução

Este projeto não foi construído apenas para funcionar --- foi
estruturado para refletir **boas práticas utilizadas em sistemas
profissionais.**

------------------------------------------------------------------------

## 🎯 Objetivo

Construir uma API robusta de ecommerce aplicando conceitos reais de
engenharia de software:

✔️ Arquitetura em camadas\
✔️ Entidades com comportamento\
✔️ Controllers finos\
✔️ Regras no domínio\
✔️ Tratamento global de exceções\
✔️ Segurança por autorização\
✔️ Infraestrutura desacoplada

------------------------------------------------------------------------

## 🧠 Filosofia Arquitetural

Este projeto segue dois princípios fundamentais:

> **Comece simples. Proteja o que é crítico.**

e

> **Você não precisa começar perfeito --- apenas evitar erros
> perigosos:** - estado público\
> - regra espalhada\
> - dependência acoplada\
> - objetos anêmicos

------------------------------------------------------------------------

## 🏗️ Arquitetura

O TechStore foi inspirado nos conceitos da **Clean Architecture**,
mantendo o domínio como centro do sistema.

    Controllers → UseCases → Domain → Repositories

### 🔵 Domain (Core)

Contém as regras de negócio e entidades:

-   Categoria\
-   Produto\
-   Cliente\
-   Pedido\
-   ItemPedido\
-   User

👉 Todas seguem o modelo:

✅ `private set`\
✅ métodos de domínio\
✅ validações internas

Resultado: um sistema mais seguro e previsível.

------------------------------------------------------------------------

### 🟢 UseCases

Responsáveis por orquestrar as operações do sistema.

Exemplo:

-   Criar categoria\
-   Buscar categoria\
-   Listar categorias

Controllers não possuem regra de negócio.

------------------------------------------------------------------------

### 🟡 Infra (Fake)

Durante a fase inicial, o projeto utiliza repositórios em memória para:

✔️ acelerar o desenvolvimento\
✔️ focar na arquitetura\
✔️ evitar complexidade prematura

Uma evolução natural será a integração com **Entity Framework Core**.

------------------------------------------------------------------------

## 🔐 Segurança

O sistema possui proteção de rotas administrativas através do:

### ✅ AdminGuard

Bloqueia acesso quando:

-   usuário não está logado\
-   usuário não é admin

Garantindo consistência de autorização.

------------------------------------------------------------------------

## ⚙️ Autenticação (Modo Demonstração)

Para simplificar testes e apresentações:

👉 O projeto utiliza um **AuthState Singleton**, mantendo o usuário
autenticado em memória.

### Por que essa decisão?

Porque arquitetura boa também significa:

> **não introduzir complexidade antes da hora.**

### Evolução planejada:

✔️ JWT\
✔️ autenticação stateless\
✔️ refresh tokens

------------------------------------------------------------------------

## 🌍 Middleware Global

### ✅ ExceptionMiddleware

Transforma exceções de domínio em respostas HTTP padronizadas:

  Exception               Status
  ----------------------- --------
  BusinessRuleException   400
  NotFoundException       404
  Exception               500

Isso torna a API muito mais previsível para quem consome.

------------------------------------------------------------------------

## 📦 Funcionalidades Implementadas

### ✔️ Autenticação

-   Login\
-   Logout\
-   Status

------------------------------------------------------------------------

### ✔️ Categorias

-   Criar\
-   Listar\
-   Buscar por Id

Com domínio protegido e validações.

------------------------------------------------------------------------

## 🚧 Próximos Passos

O projeto está evoluindo de forma incremental e arquiteturalmente
segura.

### Em desenvolvimento:

🔥 Produto --- agregado central do ecommerce\
🔥 Carrinho\
🔥 Pedido\
🔥 Persistência real\
🔥 Logs estruturados\
🔥 Cache\
🔥 JWT

------------------------------------------------------------------------

## 💡 Diferencial do Projeto

Este sistema foi construído com foco em:

👉 **pensamento de arquiteto, não apenas de programador.**

Cada decisão busca equilibrar:

-   simplicidade\
-   clareza\
-   escalabilidade\
-   manutenibilidade

------------------------------------------------------------------------

## 🛠️ Tecnologias

-   .NET\
-   ASP.NET Core Web API\
-   Swagger\
-   C#\
-   Injeção de Dependência\
-   Middleware

------------------------------------------------------------------------

## ▶️ Como Executar

``` bash
dotnet build
dotnet run
```

Acesse:

👉 `https://localhost:{porta}/swagger`

------------------------------------------------------------------------

## 📚 Documentação Arquitetural

O projeto possui documentação complementar:

-   📄 ARCHITECTURE.md\
-   📄 DECISIONS.md\
-   📄 PROJECT_OVERVIEW.md

Esses arquivos registram as decisões técnicas e o estado arquitetural do
sistema.

------------------------------------------------------------------------

## 👨‍💻 Autor

**Douglas Marcelo Monquero**\
Engenharia de Software

Desenvolvendo software com mentalidade de longo prazo.

------------------------------------------------------------------------

## ⭐ Observação Final

Este projeto representa mais do que código.

Representa a transição de:

👉 *aprender a programar*\
para\
👉 **aprender a construir sistemas.**
