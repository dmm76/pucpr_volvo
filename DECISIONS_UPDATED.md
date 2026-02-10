# 🧭 TechStore — Architecture Decision Records (ADR)

Este documento registra as principais decisões arquiteturais do projeto, seus contextos e impactos.  
Manter esse histórico reforça a previsibilidade do sistema e demonstra maturidade técnica.

---

## ADR-001 — Autenticação em memória

**Status:** Ativo (Modo demonstração acadêmica)

**Contexto:** Necessidade de autenticação simples para facilitar testes via Swagger e apresentação para banca.

**Decisão:** Utilizar `AuthState` como Singleton para manter o usuário logado durante a sessão.

**Consequências:**  
- Simples de explicar  
- Fluxo previsível para demonstração  
- Não indicado para ambientes distribuídos  

**Evolução natural:** Autenticação stateless com JWT.

---

## ADR-002 — Domínio rico

**Status:** Ativo

**Decisão:** Utilizar entidades com `private set` e métodos de domínio.

**Motivo:**  
Evitar objetos anêmicos e garantir que as regras de negócio estejam centralizadas no domínio.

**Consequências:**  
- Maior previsibilidade  
- Melhor encapsulamento  
- Código mais alinhado com práticas profissionais  

---

## ADR-003 — Infraestrutura Fake → Persistência Real

**Status:** Concluído

**Contexto:**  
Durante a fase inicial do projeto, optou-se por utilizar repositórios fake para permitir a evolução segura do domínio antes da introdução de dependências externas.

**Decisão:**  
Migrar para **Entity Framework Core + SQL Server** após a estabilização do modelo.

**Consequências:**  
- Migração sem refatorações críticas  
- Baixo acoplamento entre domínio e infraestrutura  
- Arquitetura preparada para ambientes reais  

**Observação:**  
Os repositórios fake foram mantidos apenas como apoio didático e possíveis cenários de teste.

---

## ADR-004 — Middleware Global de Exceções

**Status:** Ativo

**Decisão:** Converter exceções de domínio em respostas HTTP padronizadas.

**Benefícios:**  
- API previsível  
- Melhor experiência para consumidores  
- Facilita debug e observabilidade  

---

## ADR-005 — Guards de Autorização

**Status:** Ativo

**Decisão:** Centralizar verificação de autenticação e papel do usuário em Guards (ex.: `AdminGuard`).

**Motivo:**  
Evitar repetição de código e garantir segurança consistente.

**Consequências:**  
- Código mais limpo  
- Menor risco de falhas de autorização  
- Melhor organização das responsabilidades  

---

## ADR-006 — Ownership (Autorização por propriedade)

**Status:** Ativo

**Decisão:** Permitir que usuários acessem apenas recursos que lhes pertencem.

**Motivo:**  
Prevenir vazamento de dados e aproximar o comportamento do sistema de cenários reais.

**Impacto:**  
Eleva significativamente o nível de segurança da aplicação.

---

## ADR-007 — EF Core com Migrations Automatizadas

**Status:** Ativo

**Decisão:** Utilizar migrations versionadas para controle do schema do banco.

**Motivo:**  
Garantir previsibilidade de deploy e rastreabilidade das mudanças estruturais.

**Consequências:**  
- Maior controle sobre evolução do banco  
- Facilidade de setup do ambiente  
- Aproximação de práticas profissionais  

---

## ADR-008 — Snapshot de dados no Pedido

**Status:** Ativo

**Decisão:** Registrar nome e preço do produto no momento da compra.

**Motivo:**  
Evitar inconsistências históricas caso o produto seja alterado futuramente.

**Consequência:**  
Relatórios mais confiáveis e comportamento esperado em sistemas de e-commerce.
