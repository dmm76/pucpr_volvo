# 🏗️ Architecture — TechStore

## Visão Geral

O TechStore foi projetado com foco em previsibilidade, baixo acoplamento e clareza arquitetural.

A estratégia adotada permitiu evoluir o domínio antes da persistência, reduzindo riscos estruturais e garantindo uma migração segura para banco real.

---

## Camadas

### Domain (Core)
Responsável por concentrar as regras de negócio.

Características:

- Entidades ricas  
- Validações internas  
- Fail Fast  
- Independência de infraestrutura  

---

### Application (UseCases)
Orquestra os fluxos do sistema.

Responsabilidades:

- Coordenação entre repositórios e domínio  
- Garantia de regras de aplicação  
- Exposição de operações para a API  

---

### Infrastructure

O sistema utiliza **Entity Framework Core** como ORM principal.

A infraestrutura fake foi utilizada apenas durante a fase inicial para permitir evolução segura do domínio.  
Após a estabilização do modelo, a aplicação migrou para **SQL Server**, mantendo compatibilidade arquitetural.

Benefícios dessa abordagem:

- evolução segura do domínio  
- baixo acoplamento  
- migração sem refatorações críticas  

---

### API

Responsável pela exposição REST do sistema.

Inclui:

- Controllers organizados  
- Middleware global de exceções  
- Status codes adequados  
- Swagger para documentação  

---

## Princípios Arquiteturais

- Separation of Concerns  
- Dependency Injection  
- Rich Domain Model  
- Fail Fast  
- Baixo acoplamento  
