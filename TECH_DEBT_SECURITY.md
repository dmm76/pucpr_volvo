
# 📌 Dívida Técnica — Segurança, Autorização e Autenticação

## Contexto

O projeto **TechStore API** já demonstra uma arquitetura sólida com:

- Domínio rico  
- UseCases bem definidos  
- Middleware global  
- ErrorCodes padronizados  
- Snapshots no pedido  
- Separação clara de responsabilidades  

Com a base estrutural estável, o próximo passo natural de maturidade arquitetural é **fortalecer a segurança da aplicação**, implementando regras de autorização mais próximas de sistemas reais e evoluindo o modelo de autenticação.

---

# 🎯 Objetivo da Implementação

Evoluir o modelo atual para incluir:

✅ Autorização por papel (Role-Based Authorization)  
✅ Autorização por propriedade do recurso (**Ownership**)  
✅ Proteção contra vazamento de dados  
✅ Autenticação stateless no futuro (JWT)  
✅ Estrutura preparada para múltiplos usuários simultâneos  

---

# ⚠️ Situação Atual

Hoje o sistema possui:

### ✔ AdminGuard
Protege endpoints administrativos como cadastro de categoria.

### ✔ EF Core com persistência real
A aplicação já opera com banco relacional e migrations, garantindo integridade dos dados.

### ❗ Problema Atual
Grande parte dos endpoints ainda está **aberta para qualquer usuário**, o que permitiria, por exemplo:

- Um cliente visualizar pedidos de outro cliente  
- Acesso irrestrito a dados sensíveis  
- Falta de separação clara entre usuário comum e administrador  

Isso não representa falha arquitetural — apenas uma evolução ainda não implementada.

---

# 🔐 Autenticação Atual — AuthState Singleton

## Situação

O projeto utiliza atualmente:

```csharp
builder.Services.AddSingleton<AuthState>();
```

Essa decisão foi **intencional**, adotada para:

- Simplificar testes via Swagger  
- Permitir demonstrações rápidas  
- Focar na validação do domínio e da persistência  
- Evitar complexidade prematura  

## ⚠️ Limitações Conhecidas

Essa abordagem **não deve ser usada em produção**, pois:

- O estado de login é global no servidor  
- Não há isolamento entre usuários  
- Pode ocorrer vazamento de sessão  
- Não escala horizontalmente  
- Reiniciar a API derruba todas as sessões  

## 🧠 Decisão Arquitetural

Esta é uma **dívida técnica planejada**, não um erro.

Foi escolhida conscientemente para priorizar:

✔ Modelagem correta do domínio  
✔ Persistência confiável com EF Core  
✔ Estrutura arquitetural limpa  
✔ Evolução incremental do sistema  

> **Comece simples. Proteja o que é crítico.**

## 🚀 Evolução Planejada

Migrar para autenticação **stateless**, utilizando JWT.

### Etapas futuras:

1. Implementar geração de JWT no endpoint de login  
2. Adicionar middleware `UseAuthentication()`  
3. Popular `HttpContext.User`  
4. Criar policies (Admin / Owner)  
5. Remover completamente o `AuthState`  
6. (Opcional) Implementar Refresh Token  

---

# 🧠 Decisão Arquitetural de Segurança

Adotar dois pilares principais:

## 1️⃣ Role-Based Authorization

Controle baseado no papel do usuário.

### Admin deve poder:
- CRUD de produtos  
- CRUD de categorias  
- Visualizar todos os pedidos  
- Visualizar todos os clientes  

### Usuário comum deve poder:
- Visualizar apenas seus próprios pedidos  
- Operar somente seu carrinho  
- Acessar apenas seu cadastro  

---

## 2️⃣ Ownership (Autorização por propriedade)

O salto de maturidade do backend.

A regra central passa a ser:

> **O recurso pertence a este usuário?**  
> Se não pertence → acesso negado.

### Exemplos críticos

#### Pedidos
Usuário só pode acessar pedidos onde:

```
pedido.ClienteId == clienteIdDoUsuarioAtual
```

#### Clientes
Usuário só pode visualizar o próprio registro.

---

# 🏗️ Estratégia de Implementação

## ✔ Criar UserGuard

Semelhante ao AdminGuard.

Responsável por garantir:

- Usuário autenticado  
- Bloqueio com 401 quando necessário  

---

## ✔ Resolver Cliente do Usuário Logado

Criar método utilitário:

```csharp
private int GetClienteIdDoUsuarioAtualOrThrow()
{
    var userId = _auth.UserId;

    var cliente = _clienteRepo.BuscarTodos()
        .FirstOrDefault(c => c.UserId == userId);

    if (cliente is null)
        throw new NotFoundException(ErrorCodes.ClienteNotFound);

    return cliente.Id;
}
```

Esse método será a base do ownership.

---

# 🔐 Endpoints que DEVEM ser protegidos

## 🔴 Alta prioridade

### Pedidos
- GET /api/pedidos/{id}  
- GET /api/pedidos/cliente/{clienteId}  
- POST /itens  
- PUT cliente  
- PUT endereço  
- PUT pagamento  
- POST confirmar  
- POST pagar  

👉 Somente o dono do carrinho deve operar.

---

## 🟡 Média prioridade

### Clientes
- GET /api/clientes → Admin only  
- GET /api/clientes/{id}  
    - Admin: pode  
    - Usuário: apenas o próprio  

---

## 🟢 Baixa prioridade (podem permanecer públicos)

- GET produtos  
- GET categorias  
- Criar carrinho (modo visitante — comum em ecommerces)  

---

# 🧱 Separação Recomendada de Rotas

Para elevar o nível profissional da API:

### Rotas Públicas
```
/api/produtos  
/api/categorias  
/api/pedidos (criar carrinho)  
```

### Rotas do Usuário
```
/api/pedidos/{id}  
/api/pedidos/cliente/{clienteId}  
```

### Rotas Admin
```
/api/admin/produtos  
/api/admin/categorias  
/api/admin/pedidos  
```

Separar rotas melhora:

✅ Clareza  
✅ Segurança  
✅ Organização mental do sistema  

---

# 🚀 Benefícios Após Implementação

O projeto passa a demonstrar:

✅ Segurança real de backend  
✅ Controle de acesso profissional  
✅ Prevenção de vazamento de dados  
✅ Arquitetura preparada para produção  
✅ Mentalidade de engenheiro de software  

Este é um dos maiores saltos de maturidade de um backend.

---

# 📊 Prioridade da Dívida

## 🔥 ALTA

Recomenda-se implementar **antes da evolução para autenticação JWT**, garantindo que:

- O domínio já nasça protegido  
- As regras de acesso estejam consolidadas  
- A migração para stateless seja apenas infraestrutura  

---

# 🧭 Próximo Passo Recomendado

Ordem ideal:

1️⃣ Implementar UserGuard  
2️⃣ Aplicar ownership nos pedidos  
3️⃣ Proteger endpoints de clientes  
4️⃣ Criar rotas admin para listagens globais  
5️⃣ Migrar autenticação para JWT  

---

# ⭐ Conclusão

O TechStore já possui uma base arquitetural **acima da média**.

Esta dívida técnica não corrige um erro —  
ela representa o próximo nível de maturidade do sistema.

> **Segurança não é um detalhe.  
Ela é parte do design.**
