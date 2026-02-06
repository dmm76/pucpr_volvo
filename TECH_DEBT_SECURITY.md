
# 📌 Dívida Técnica — Segurança e Autorização (Ownership + Roles)

## Contexto

O projeto **TechStore API** já demonstra uma arquitetura sólida com:

- Domínio rico
- UseCases bem definidos
- Middleware global
- ErrorCodes padronizados
- Snapshots no pedido
- Separação clara de responsabilidades

Com a base estrutural estável, o próximo passo natural de maturidade arquitetural é **fortalecer a segurança da aplicação**, implementando regras de autorização mais próximas de sistemas reais.

---

# 🎯 Objetivo da Implementação

Evoluir o modelo atual de autenticação para incluir:

✅ Autorização por papel (Role-Based Authorization)  
✅ Autorização por propriedade do recurso (**Ownership**)  
✅ Proteção contra vazamento de dados  
✅ Preparação arquitetural para JWT no futuro  

---

# ⚠️ Situação Atual

Hoje o sistema possui:

### ✔ AdminGuard
Protege endpoints administrativos como cadastro de categoria.

### ❗ Problema Atual
Grande parte dos endpoints ainda está **aberta para qualquer usuário**, o que permitiria, por exemplo:

- Um cliente visualizar pedidos de outro cliente
- Acesso irrestrito a dados sensíveis
- Falta de separação clara entre usuário comum e administrador

Isso não é um erro de arquitetura — apenas um passo ainda não implementado.

---

# 🧠 Decisão Arquitetural

Adotar dois pilares de segurança:

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

Este é o salto de maturidade.

A regra central passa a ser:

> **O recurso pertence a este usuário?**

Se não pertence → acesso negado.

### Exemplos críticos:

#### Pedidos
- Usuário só pode acessar pedidos onde:
```
pedido.ClienteId == clienteIdDoUsuarioAtual
```

#### Clientes
- Usuário só pode visualizar o próprio registro.

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
- Criar carrinho (opcional — muitos ecommerces permitem modo visitante)

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

# ⚠️ Observação Importante

O projeto utiliza atualmente:

## AuthState Singleton

Isso é **aceitável para demonstração**, mas representa uma dívida técnica planejada.

### Evolução futura:
- JWT
- Autenticação stateless
- Refresh tokens

👉 Nenhuma mudança necessária agora.

Evitar complexidade prematura é uma decisão arquitetural madura.

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

Recomenda-se implementar **antes da migração para EF Core**, pois:

- Evita refatorações futuras
- Mantém o domínio protegido
- Garante que persistência já nasça segura

---

# 🧭 Próximo Passo Recomendado

Ordem ideal:

1️⃣ Implementar UserGuard  
2️⃣ Aplicar ownership nos pedidos  
3️⃣ Proteger endpoints de clientes  
4️⃣ Criar rotas admin para listagens globais  

Depois disso:

👉 EF Core será apenas uma troca de infraestrutura — não uma mudança de comportamento.

---

# ⭐ Conclusão

O TechStore já possui base arquitetural acima da média.

Esta dívida técnica não corrige um erro —  
ela representa o próximo nível de maturidade do sistema.

> **Segurança não é um detalhe.  
Ela é parte do design.**
