# 🖥️ TechStore API

Backend arquitetado para um e-commerce moderno de informática.

> “Comece simples. Proteja o que é crítico.”

---

## 🧠 Sobre o Projeto

O **TechStore** é uma Web API desenvolvida em **.NET**, construída com mentalidade arquitetural desde o início.  
O projeto evoluiu de uma infraestrutura fake — utilizada estrategicamente para permitir o amadurecimento do domínio — para **persistência real com Entity Framework Core e SQL Server**, atendendo integralmente aos requisitos do projeto final.

Essa abordagem demonstra uma evolução controlada da arquitetura, reduzindo acoplamento e evitando refatorações de alto risco.

---

## 🏗️ Arquitetura

O projeto segue uma separação clara de responsabilidades:

- **Core (Domínio)** → Entidades ricas e regras de negócio centralizadas  
- **UseCases (Aplicação)** → Orquestração das operações do sistema  
- **Infra (Persistência)** → EF Core + SQL Server  
- **API** → Controllers REST com validações e middleware global  

Princípios aplicados:

- Separation of Concerns  
- Dependency Injection  
- Fail Fast  
- Domínio rico (Rich Domain Model)  

---

## 🗄️ Modelo do Banco de Dados

![Modelo do Banco](utils/images/Tabelas.JPG)

---

## ⚙️ Tecnologias

- .NET  
- ASP.NET Core  
- Entity Framework Core  
- SQL Server  
- Swagger  

---

## 🧱 Persistência

O sistema utiliza **Entity Framework Core** com migrations versionadas, garantindo:

- integridade relacional  
- versionamento do banco  
- comportamento próximo de ambientes reais  
- previsibilidade de deploy  

Os repositórios fake foram mantidos apenas como apoio didático e para possíveis cenários de teste.

---

## 🔐 Segurança

O projeto implementa:

- Autorização baseada em papéis (Admin/User)  
- Ownership (usuário acessa apenas seus próprios recursos)  
- Guards de proteção nos endpoints críticos  

O uso atual de `AuthState` em memória é uma decisão consciente para simplificação do fluxo acadêmico, com evolução planejada para autenticação stateless (JWT).

---

## 🚀 Funcionalidades Implementadas

### Produtos
✅ CRUD completo  
✅ Paginação (skip/take)  
✅ Filtros opcionais  
✅ Validação de categoria  

### Pedidos
✅ Carrinho  
✅ Baixa automática de estoque  
✅ Snapshot de preço  
✅ Proteção contra estoque insuficiente  

### Relatórios
✅ Total vendido por categoria  

---

## 📌 Decisões Arquiteturais

O projeto prioriza domínio rico e regras de negócio centralizadas nas entidades, evitando lógica em controllers e favorecendo manutenibilidade.

Decisões detalhadas podem ser consultadas em:

👉 `DECISIONS.md`

---

## 📄 Dívidas Técnicas

Dívidas técnicas são tratadas de forma transparente e documentadas:

- `TECH_DEBT_PT.md`  
- `TECH_DEBT_SECURITY.md`  

Essa prática reforça a maturidade do projeto e facilita sua evolução futura.
