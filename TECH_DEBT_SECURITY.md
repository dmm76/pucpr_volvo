# 🔐 Tech Debt — Security

## Status Atual

✔ Role-based authorization implementada  
✔ Ownership implementado  
✔ Guards aplicados  
⚠ AuthState em memória (evolução planejada para JWT)  

---

## Contexto

O uso de AuthState foi uma decisão consciente para simplificar o fluxo de autenticação durante o desenvolvimento acadêmico.

Apesar disso, a arquitetura já está preparada para evolução futura.

---

## Próximos Passos (Evolução Natural)

- Autenticação stateless com JWT  
- Refresh tokens  
- Policies mais refinadas  
- Hardening de segurança  

Essa dívida é intencional e não compromete os objetivos atuais do projeto.
