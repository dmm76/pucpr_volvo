
# 🖥️ TechStore API

### Backend arquitetado para um ecommerce moderno de informática

> "Comece simples. Proteja o que é crítico."

------------------------------------------------------------------------

## 🚀 Sobre o Projeto

O **TechStore** é uma Web API desenvolvida em **.NET**, projetada desde
o início com **mentalidade arquitetural**, priorizando:

- Domínio rico  
- Separação de responsabilidades  
- Previsibilidade  
- Código sustentável  
- Facilidade de evolução  

Este projeto não foi construído apenas para funcionar — foi
estruturado para refletir **boas práticas utilizadas em sistemas
profissionais.**

------------------------------------------------------------------------

## 🎯 Objetivo

Construir uma API robusta de ecommerce aplicando conceitos reais de
engenharia de software:

✔️ Arquitetura em camadas  
✔️ Entidades com comportamento  
✔️ Controllers finos  
✔️ Regras no domínio  
✔️ Tratamento global de exceções  
✔️ Segurança por autorização  
✔️ Infraestrutura desacoplada  

------------------------------------------------------------------------

## 🧠 Filosofia Arquitetural

Este projeto segue dois princípios fundamentais:

> **Comece simples. Proteja o que é crítico.**

e

> **Você não precisa começar perfeito — apenas evitar erros perigosos:**  
> - estado público  
> - regra espalhada  
> - dependência acoplada  
> - objetos anêmicos  

------------------------------------------------------------------------

## 🏗️ Arquitetura

O TechStore foi inspirado nos conceitos da **Clean Architecture**,
mantendo o domínio como centro do sistema.

```
Controllers → UseCases → Domain → Repositories
```

### 🔵 Domain (Core)

Contém as regras de negócio e entidades:

- Categoria  
- Produto  
- Cliente  
- Endereco  
- Pedido  
- ItemPedido  
- User  

👉 Todas seguem o modelo:

✅ `private set`  
✅ métodos de domínio  
✅ validações internas  

Resultado: um sistema mais seguro e previsível.

------------------------------------------------------------------------

### 🟢 UseCases

Responsáveis por orquestrar as operações do sistema.

Exemplos:

- Criar carrinho  
- Adicionar itens  
- Identificar cliente  
- Definir endereço  
- Confirmar pedido  
- Realizar pagamento  

Controllers não possuem regra de negócio.

------------------------------------------------------------------------

### 🟡 Infra (Fake)

Durante a fase inicial, o projeto utiliza repositórios em memória para:

✔️ acelerar o desenvolvimento  
✔️ focar na arquitetura  
✔️ evitar complexidade prematura  

Uma evolução natural será a integração com **Entity Framework Core**.

------------------------------------------------------------------------

## 🔐 Segurança

O sistema possui proteção de rotas administrativas através do:

### ✅ AdminGuard

Bloqueia acesso quando:

- usuário não está logado  
- usuário não é admin  

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

✔️ JWT  
✔️ autenticação stateless  
✔️ refresh tokens  

------------------------------------------------------------------------

## 🌍 Middleware Global

### ✅ ExceptionMiddleware

Transforma exceções de domínio em respostas HTTP padronizadas:

| Exception               | Status |
|------------------------|--------|
| BusinessRuleException  | 400    |
| NotFoundException      | 404    |
| Exception              | 500    |

Isso torna a API muito mais previsível para quem consome.

------------------------------------------------------------------------

## 📦 Funcionalidades Implementadas

### ✔️ Autenticação
- Login  
- Logout  
- Status  

---

### ✔️ Categorias
- Criar  
- Listar  
- Buscar por Id  

---

### ✔️ Produtos
- Cadastro (admin)  
- Listagem pública  
- Busca por Id  
- Validações de domínio  
- Controle de estoque  

---

### ✔️ Clientes
- Cadastro com criação automática de User  
- Seed inicial com clientes e endereços  
- Associação User ↔ Cliente  
- Estrutura preparada para múltiplos endereços  

---

### ✔️ Carrinho / Pedido (Checkout Completo)

Fluxo real de ecommerce implementado:

✔️ Criar carrinho  
✔️ Adicionar e remover itens  
✔️ Snapshot do preço do produto  
✔️ Identificação automática do cliente (snapshot do nome)  
✔️ Snapshot do endereço de entrega  
✔️ Atalho para usar endereço padrão  
✔️ Definição de forma de pagamento  
✔️ Validação de regras antes da confirmação  
✔️ Baixa automática de estoque ao pagar  

👉 O pedido preserva **snapshots**, garantindo histórico mesmo que os dados do cliente mudem.

------------------------------------------------------------------------

## 🔥 Diferenciais Arquiteturais

Este projeto demonstra práticas vistas em sistemas profissionais:

✅ Domínio rico (não anêmico)  
✅ Separação clara de camadas  
✅ UseCases enxutos  
✅ Controllers mínimos  
✅ Middleware global  
✅ ErrorCodes padronizados  
✅ Tradução centralizada de erros  
✅ Seed para facilitar demonstrações  
✅ Checkout com comportamento real  

------------------------------------------------------------------------

## 🧪 Roteiro de Demonstração (Swagger)

Fluxo recomendado para apresentação:

### 1️⃣ Conferir dados
```
GET /api/clientes
GET /api/produtos
```

### 2️⃣ Criar carrinho
```
POST /api/pedidos
```

### 3️⃣ Adicionar item
```
POST /api/pedidos/{pedidoId}/itens
{
  "produtoId": 1,
  "quantidade": 2
}
```

### 4️⃣ Identificar cliente (snapshot automático)
```
PUT /api/pedidos/{pedidoId}/cliente
{
  "clienteId": 1
}
```

### 5️⃣ Usar endereço padrão (atalho premium)
```
PUT /api/pedidos/{pedidoId}/usar-endereco-padrao/1
```

### 6️⃣ Definir pagamento
```
PUT /api/pedidos/{pedidoId}/pagamento
{
  "formaPagamento": 1
}
```

### 7️⃣ Confirmar e pagar
```
POST /api/pedidos/{pedidoId}/confirmar
POST /api/pedidos/{pedidoId}/pagar
```

### 8️⃣ Provar baixa de estoque
```
GET /api/produtos/{id}
```

👉 Resultado esperado: estoque reduzido.

------------------------------------------------------------------------

## 🛠️ Tecnologias

- .NET  
- ASP.NET Core Web API  
- Swagger  
- C#  
- Injeção de Dependência  
- Middleware  

------------------------------------------------------------------------

## ▶️ Como Executar

```bash
dotnet build
dotnet run
```

Acesse:

👉 `https://localhost:{porta}/swagger`

------------------------------------------------------------------------

## 📚 Documentação Arquitetural

O projeto possui documentação complementar:

- 📄 ARCHITECTURE.md  
- 📄 DECISIONS.md  
- 📄 PROJECT_OVERVIEW.md  

Esses arquivos registram as decisões técnicas e o estado arquitetural do
sistema.

------------------------------------------------------------------------

## 👨‍💻 Autor

**Douglas Marcelo Monquero**  
Engenharia de Software  

Desenvolvendo software com mentalidade de longo prazo.

------------------------------------------------------------------------

## ⭐ Observação Final

Este projeto representa mais do que código.

Representa a transição de:

👉 *aprender a programar*  
para  
👉 **aprender a construir sistemas.**
