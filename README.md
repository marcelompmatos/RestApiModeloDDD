# RestApiModeloDDD

Projeto de estudo focado em arquitetura corporativa utilizando DDD (Domain-Driven Design) com o ecossistema :contentReference[oaicite:0]{index=0}.

Arquitetura de sistemas corporativos costuma ser menos explorada do que APIs REST simples, mas é uma base essencial para aplicações escaláveis, organizadas e de fácil manutenção.

Este projeto demonstra a construção de uma API utilizando separação por camadas, padrões de projeto e boas práticas aplicadas em sistemas reais.

---

## Tecnologias utilizadas

- :contentReference[oaicite:1]{index=1}
- :contentReference[oaicite:2]{index=2}
- :contentReference[oaicite:3]{index=3}
- :contentReference[oaicite:4]{index=4}
- xUnit
- :contentReference[oaicite:5]{index=5}

---

## Objetivo

Demonstrar uma arquitetura padrão para aplicações corporativas utilizando:

- DDD
- Repository Pattern
- Service Layer
- DTO
- Dependency Injection
- Mapeamento entre camadas
- Testes automatizados
- Boas práticas com .NET

---

## Estrutura da solução

```text
RestApiModeloDDD

├── RestApiModeloDDD.API
│   └── Controllers
│       ├── ClienteController.cs
│       └── ProdutoController.cs

├── RestApiModeloDDD.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Mappers
│   └── Services

├── RestApiModeloDDD.Domain
│   ├── Entities
│   ├── Interfaces
│   │   ├── Repositories
│   │   └── Services
│   ├── Services
│   └── Validations

├── RestApiModeloDDD.Infrastructure
│   ├── IoC
│   └── Data
│       ├── Context
│       └── Repositories

└── RestApiModeloDDD.Tests
```

---

## Camadas

### API
Responsável pela exposição da aplicação através de endpoints HTTP.

### Application
Responsável pela orquestração entre apresentação e domínio.

### Domain
Responsável pelas regras de negócio, entidades e contratos.

### Infrastructure
Responsável por persistência, acesso a dados e injeção de dependência.

### Tests
Responsável pela validação e testes automatizados.

---

## Conceitos abordados

- DDD
- SOLID
- Clean Code
- Repository Pattern
- Service Pattern
- Dependency Injection
- AutoMapper
- Entity Framework Core
- Testes unitários

---

## Finalidade

Servir como modelo de arquitetura para APIs corporativas utilizando .NET, com foco em:

- organização
- escalabilidade
- manutenção
- reutilização
- separação de responsabilidades

---

## Autor

Projeto desenvolvido para fins de estudo e aprofundamento em arquitetura de software com .NET.