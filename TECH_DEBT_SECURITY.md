# 🔐 Security Technical Debt — TechStore

Este documento registra os pontos de segurança que ainda não foram totalmente implementados no **TechStore**, bem como as decisões conscientes relacionadas a eles.

O objetivo não é apenas listar lacunas — mas garantir que a evolução da segurança ocorra de forma planejada e sem comprometer a arquitetura.

> Segurança não é um estado final — é um processo contínuo.

---

## 🧭 Postura Atual de Segurança

O TechStore foi projetado adotando o princípio de **segurança por design**, evitando tratar proteção como uma camada adicionada posteriormente.

Atualmente, o sistema já possui:

✔ autorização baseada em papéis (**Admin / User**)  
✔ ownership garantindo isolamento de dados  
✔ guards aplicados em endpoints críticos  
✔ validações de domínio protegendo regras sensíveis  
✔ middleware centralizado reduzindo vazamento de detalhes internos  

> O objetivo inicial foi reduzir riscos estruturais antes de introduzir complexidade adicional.

---

## ⚠️ Débito Principal — Autenticação Stateful

### Status: Intencional (modo acadêmico / demonstrativo)

---

### Contexto

O sistema utiliza atualmente um mecanismo de autenticação em memória para simplificar o fluxo de desenvolvimento e facilitar demonstrações via Swagger.

Essa decisão permitiu:

- reduzir complexidade prematura  
- acelerar validações arquiteturais  
- manter foco nas regras de negócio  

---

### Riscos Conhecidos

Embora adequado para o contexto atual, este modelo apresenta limitações claras:

⚠ não escalável  
⚠ dependente do ciclo de vida da aplicação  
⚠ incompatível com ambientes distribuídos  
⚠ não resiliente a reinicializações  

---

### Justificativa Arquitetural

A autenticação foi tratada como uma **complexidade adiável**, não como uma omissão.

Introduzir JWT prematuramente poderia:

- aumentar o custo de desenvolvimento  
- gerar distrações arquiteturais  
- deslocar o foco do domínio  

> Nem toda ausência é uma falha — algumas são decisões de timing.

---

## 🚀 Evolução Planejada

A arquitetura já foi preparada para uma transição segura para um modelo stateless.

### Próximo passo natural:

✔ autenticação com JWT  
✔ refresh tokens  
✔ políticas de autorização mais granulares  
✔ expiração e rotação de tokens  

Essa migração pode ocorrer sem refatorações estruturais relevantes.

---

## 🔒 Próximos Endurecimentos de Segurança (Hardening)

À medida que o sistema evoluir, recomenda-se a adoção de práticas adicionais:

- rate limiting  
- proteção contra brute force  
- logs de auditoria  
- monitoramento de acessos  
- headers de segurança  
- segregação de ambientes  
- gestão segura de secrets  

Antecipar essas medidas reduz o custo de crescimento.

---

## 🧠 Estratégia de Segurança

O TechStore evita dois extremos perigosos:

### ❌ Ignorar riscos  
### ❌ Introduzir complexidade desnecessária cedo demais  

A abordagem adotada é:

> **segurança progressiva e arquiteturalmente sustentável.**

Cada camada de proteção deve surgir no momento em que agrega valor real ao sistema.

---

## 💡 Filosofia

> Segurança eficaz não depende apenas de ferramentas —  
> mas da clareza das decisões arquiteturais.

> Sistemas seguros não são os que possuem mais mecanismos,  
> e sim os que entendem seus riscos.
