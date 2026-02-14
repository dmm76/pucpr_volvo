# 🏗️ Architecture — TechStore

## 🎯 Objetivo Arquitetural

Construir um backend previsível, testável e preparado para evolução, evitando decisões que aumentem o custo de mudança ao longo do tempo.

O TechStore foi projetado com foco em:

- baixo acoplamento  
- domínio consistente  
- separação clara de responsabilidades  
- estabilidade estrutural  

> Arquitetura não elimina complexidade — ela impede que a complexidade vire caos.

---

## 🧠 Visão Geral

O sistema segue uma organização em camadas, onde cada parte possui responsabilidades bem definidas:


Essa estrutura permite evolução segura do software sem comprometer o domínio.

---

## 🧱 Camadas da Arquitetura

### 🔵 Domain (Core)

Coração do sistema — responsável pelas regras de negócio.

**Características:**

✔ Entidades ricas  
✔ Validações internas  
✔ Fail Fast  
✔ Independência de frameworks  
✔ Proteção contra modelos anêmicos  

**Objetivo:** garantir que a lógica crítica permaneça centralizada e protegida.

---

### 🟣 Application (UseCases)

Responsável por orquestrar os fluxos do sistema.

**Responsabilidades:**

- coordenação entre domínio e persistência  
- execução dos casos de uso  
- aplicação de regras de aplicação  
- exposição de operações para a API  

> Controllers não contêm regra de negócio.

Isso mantém o sistema previsível e testável.

---

### 🟠 Infrastructure

Camada responsável por preocupações técnicas externas ao domínio.

O TechStore utiliza:

👉 **Entity Framework Core** como ORM  
👉 **SQL Server** como banco relacional  

### Estratégia adotada

O projeto iniciou com repositórios fake para acelerar validações arquiteturais e permitir a evolução do domínio sem dependência de banco.

Após a estabilização do modelo:

👉 migração segura para persistência real.

**Benefícios dessa abordagem:**

✔ evolução controlada  
✔ baixo risco estrutural  
✔ domínio preservado  
✔ ausência de refatorações críticas  

---

### 🔴 API

Responsável pela exposição REST do sistema.

**Inclui:**

- Controllers organizados  
- Middleware global de exceções  
- Status codes coerentes  
- Swagger para documentação  

### Princípio importante

👉 A API atua como camada de entrega — não como dona das regras.

---

## ⭐ Decisões Arquiteturais Relevantes

Algumas decisões foram fundamentais para garantir a sustentabilidade do sistema:

### ✔ Domínio Rico
Regras vivem nas entidades — não espalhadas pelo sistema.

---

### ✔ Middleware Global de Exceções
Centraliza erros e garante respostas consistentes.

👉 O sistema reage de forma previsível, independente de onde o erro ocorreu.

---

### ✔ Separação entre Domínio e Infraestrutura
O Core não conhece banco, frameworks ou detalhes técnicos.

Isso reduz drasticamente o acoplamento.

---

### ✔ Evolução Progressiva da Persistência
Fake → EF Core → SQL Server.

Arquitetura pensada para crescer sem ruptura.

---

## 🔐 Segurança Arquitetural

O sistema foi projetado considerando isolamento e proteção de recursos:

- autorização baseada em papéis  
- ownership  
- guards de acesso  
- preparação para autenticação stateless  

Segurança não foi tratada como extensão — mas como parte do design.

---

## 🚀 Fluxo Crítico do Sistema

O fluxo de pedidos recebeu maior rigor arquitetural por ser a área mais sensível do negócio.

**Proteções implementadas:**

✔ snapshot de preço  
✔ validação de estoque  
✔ prevenção de inconsistências  
✔ vínculo correto do pedido ao usuário  

> Software crítico exige previsibilidade — não improviso.

---

## 📈 Estratégia de Evolução

A arquitetura do TechStore foi pensada para permitir crescimento sem aumento descontrolado de complexidade.

**Próximos passos naturais:**

- autenticação com JWT  
- observabilidade  
- testes automatizados  
- cache  
- políticas de segurança mais refinadas  

---

## 🧭 Princípios Norteadores

- Separation of Concerns  
- Dependency Injection  
- Rich Domain Model  
- Fail Fast  
- Baixo acoplamento  
- Alta coesão  

---

## 💡 Filosofia Arquitetural

> Prefira uma arquitetura simples e evolutiva  
> a uma arquitetura complexa e frágil.

> Decisões conscientes hoje evitam refatorações traumáticas amanhã.
