# 📘 Architecture Decision Records — TechStore

Este documento registra as principais decisões arquiteturais do projeto **TechStore**.

O objetivo é preservar o contexto técnico, reduzir dependência de conhecimento implícito e facilitar a evolução segura do sistema.

> Decisões não documentadas se tornam decisões esquecidas.

---

## 🧭 Como ler este documento

Cada ADR descreve:

- contexto do problema  
- decisão tomada  
- consequências  
- direção futura  

O foco não é apenas **o que foi decidido**, mas **por que foi decidido**.

---

# ADR-001 — Autenticação em Memória

**Status:** Ativo (modo demonstração)  
**Data:** 2026  

## Contexto
Era necessário implementar autenticação rapidamente para permitir testes via Swagger e viabilizar a apresentação do sistema sem aumentar a complexidade inicial.

## Decisão
Utilizar um `AuthState` como Singleton para manter o estado de autenticação em memória.

## Consequências

### Positivas
✔ implementação simples  
✔ fluxo fácil de demonstrar  
✔ baixo custo de manutenção inicial  

### Negativas
⚠ não escalável  
⚠ não adequado para produção  
⚠ dependente de estado da aplicação  

## Evolução Planejada
Migrar para autenticação stateless utilizando **JWT**.

> Decisão consciente para reduzir complexidade prematura.

---

# ADR-002 — Domínio Rico

**Status:** Ativo  

## Contexto
Sistemas com regras espalhadas tendem a se tornar imprevisíveis e difíceis de manter.

## Decisão
Centralizar regras de negócio nas entidades, utilizando:

- `private set`  
- métodos de domínio  
- validações internas  

## Consequências

### Positivas
✔ maior previsibilidade  
✔ proteção contra modelos anêmicos  
✔ regras próximas dos dados  
✔ código mais expressivo  

### Trade-off
⚠ curva de aprendizado maior para quem não está habituado ao padrão.

> Preferimos complexidade localizada no domínio do que complexidade espalhada pelo sistema.

---

# ADR-003 — Uso Inicial de Repositórios Fake

**Status:** Concluído (fase de transição encerrada)

## Contexto
No início do projeto, o domínio ainda estava em evolução. Acoplar o sistema prematuramente ao banco poderia gerar refatorações caras.

## Decisão
Utilizar repositórios fake para permitir:

- validação rápida das regras  
- testes manuais previsíveis  
- evolução segura do modelo  

## Consequências

### Positivas
✔ maior velocidade de desenvolvimento  
✔ domínio estabilizado antes da persistência  
✔ baixo risco estrutural  

### Negativas
⚠ ausência de validação relacional real durante a fase inicial  

## Resultado
Após a estabilização do domínio, o sistema migrou com segurança para **Entity Framework Core + SQL Server**, sem necessidade de refatorações críticas.

> Evolução arquitetural controlada reduz riscos futuros.

---

# ADR-004 — Middleware Global de Exceções

**Status:** Ativo  

## Contexto
Permitir que cada controller trate exceções gera inconsistência nas respostas da API e aumenta a duplicação de código.

## Decisão
Centralizar o tratamento de exceções em um middleware global, responsável por converter erros de domínio em respostas HTTP padronizadas.

## Consequências

### Positivas
✔ respostas previsíveis  
✔ melhor experiência para consumidores da API  
✔ redução de código repetido  
✔ facilidade de observabilidade futura  

### Trade-off
⚠ exige disciplina na criação de exceções de domínio.

> O sistema deve reagir de forma consistente — independente de onde o erro ocorreu.

---

# ADR-005 — Guard para Rotas Administrativas

**Status:** Ativo  

## Contexto
Repetir validações de autorização nos controllers aumenta o risco de falhas de segurança.

## Decisão
Criar um **AdminGuard** para centralizar a verificação de login e permissões.

## Consequências

### Positivas
✔ segurança consistente  
✔ redução de duplicação  
✔ regras de acesso explícitas  

### Trade-off
⚠ pequena camada adicional no fluxo de requisição.

> Segurança não deve depender de memória do desenvolvedor — mas da arquitetura.

---

## 📈 Direção Arquitetural

As decisões do TechStore seguem um princípio central:

> **Evitar complexidade prematura, sem comprometer a evolução futura.**

A arquitetura foi pensada para crescer de forma sustentável, priorizando previsibilidade e baixo acoplamento.

---

## 💡 Filosofia de Decisão

Antes de introduzir qualquer tecnologia ou padrão, uma pergunta guia o processo:

> **Isso reduz ou aumenta o custo de mudança do sistema?**

Se aumentar — provavelmente é cedo demais.

Se reduzir — provavelmente é arquitetura.
