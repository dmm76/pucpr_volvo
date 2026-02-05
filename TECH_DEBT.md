# TECH_DEBT.md

## Technical Debt Register

This document tracks known technical debts in the **TechStore**
project.\
All items listed here are **intentional decisions** made to prioritize
delivery of core business functionality while maintaining a clean and
evolvable architecture.

------------------------------------------------------------------------

## 1. Automated Tests Using Fake Repositories

**Status:** Pending\
**Priority:** Medium\
**Risk Level:** Low (current academic context)

### Context

Fake repositories were implemented to simulate persistence and allow
fast, isolated execution of business logic without requiring a database.

Although the architecture already supports testing, automated tests have
been postponed to prioritize the implementation of core domain flows.

### Why This Is Acceptable

-   The architecture is already testable.
-   Infrastructure is properly isolated from the Core.
-   Fake repositories allow deterministic behavior.
-   No structural compromise was introduced.

### Impact

Current impact is minimal because:

-   The project is in an academic / demonstration phase.
-   Business rules remain centralized in the domain.
-   Repositories can be swapped without affecting the application layer.

### Future Plan

Implement automated tests focusing on business behavior:

-   Unit tests for **ProdutoUseCases**
-   Unit tests for **PedidoUseCases**
-   Validation of domain invariants
-   Error flow validation
-   Fake repository scenario coverage

------------------------------------------------------------------------

## Guiding Principle

> **"Prefer conscious technical debt over accidental architecture."**

Technical debt is acceptable when:

-   It is documented
-   It is intentional
-   It does not compromise system design
-   There is a clear path for resolution

------------------------------------------------------------------------

## Architectural Note

The current architecture follows these principles:

-   Domain protected from infrastructure concerns\
-   Replaceable persistence layer\
-   Fake infrastructure used strictly for testing and development\
-   No domain adaptation to support fake implementations

This ensures that future improvements --- including automated tests ---
can be added safely without requiring structural refactoring.

------------------------------------------------------------------------

**Last Updated:** 2026-02-05
