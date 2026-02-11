# 📌 Technical Debt — TechStore

Este documento registra os débitos técnicos conhecidos do **TechStore**.

Todos os itens listados são **decisões conscientes**, tomadas para priorizar a estabilidade arquitetural e a entrega dos fluxos críticos do sistema.

> Débito técnico não é sinal de negligência —  
> é o resultado de decisões intencionais sob restrições reais.

---

## 🧭 Princípio Norteador

O TechStore adota uma abordagem pragmática:

✔ documentar  
✔ entender o impacto  
✔ definir um caminho de evolução  

Débitos se tornam perigosos apenas quando são invisíveis.

---

# 🔎 Débitos Técnicos Atuais

---

## 1️⃣ Ausência de Testes Automatizados

**Status:** Pendente  
**Prioridade:** Média  
**Risco:** Baixo (contexto acadêmico / demonstrativo)

---

### Contexto

A arquitetura já foi projetada para ser testável — com domínio isolado e baixo acoplamento — porém a implementação dos testes foi adiada para priorizar a consolidação das regras de negócio.

A decisão evitou investir em testes enquanto o modelo ainda estava em evolução.

---

### Justificativa Arquitetural

- o Core permanece independente  
- regras estão centralizadas  
- repositórios são substituíveis  
- comportamento é previsível  

Nenhum atalho estrutural foi introduzido.

---

### Impacto Atual

Baixo, pois:

- o sistema encontra-se estável  
- o projeto possui caráter acadêmico  
- o domínio está protegido  

---

### Plano de Evolução

Implementar testes focados em comportamento:

✔ testes unitários para UseCases  
✔ validação das invariantes do domínio  
✔ testes de fluxos críticos  
✔ cenários de erro  
✔ cobertura com repositórios fake  

> Testes devem proteger o domínio — não apenas aumentar métricas.

---

## 📈 Estratégia de Gestão do Débito

O TechStore evita dois extremos perigosos:

### ❌ Ignorar o débito  
### ❌ Tentar eliminá-lo prematuramente  

A estratégia adotada é **débitos conscientes e controlados**.

Antes de assumir qualquer débito, uma pergunta guia a decisão:

> **Isso compromete a arquitetura ou apenas adia uma melhoria?**

Se comprometer — não é débito.  
É erro de design.

---

## 🧠 Observação Arquitetural

A arquitetura atual garante que os débitos possam ser resolvidos sem refatorações traumáticas, pois mantém:

✔ domínio isolado  
✔ persistência desacoplada  
✔ responsabilidades bem definidas  
✔ ausência de adaptações estruturais temporárias  

Isso preserva a capacidade de evolução do sistema.

---

## 🚀 Próximos Débitos Prováveis (Evolução Natural)

À medida que o sistema crescer, novos débitos podem surgir — de forma planejada — como parte da evolução do software:

- testes de integração  
- observabilidade  
- logs estruturados  
- cache  
- políticas avançadas de segurança  

Antecipar esses pontos reduz o custo de mudança.

---

## 💡 Filosofia

> Prefira um débito técnico consciente  
> a uma arquitetura acidental.

> Software sustentável não é o que não possui débitos —  
> é o que consegue administrá-los.
