# RestApiModeloDDD

Arquitetura de sistemas corporativos é um tema muitas vezes menos explorado do que APIs REST, embora seja fundamental para aplicações escaláveis, organizadas e de fácil manutenção.

Neste projeto, apresento a construção de uma arquitetura baseada em DDD (Domain-Driven Design), utilizando tecnologias modernas do ecossistema .NET para demonstrar boas práticas de separação de responsabilidades, organização em camadas e padrões de projeto aplicados ao mundo real.

## Tecnologias utilizadas

- :contentReference[oaicite:1]{index=1}
- :contentReference[oaicite:2]{index=2}
- :contentReference[oaicite:3]{index=3}
- :contentReference[oaicite:4]{index=4}
- xUnit
- SQL Server

## Objetivo

Demonstrar uma arquitetura padrão para aplicações corporativas utilizando:

- DDD (Domain-Driven Design)
- Repository Pattern
- Service Layer
- DTO
- Dependency Injection
- Mapeamento entre camadas
- Testes automatizados
- Boas práticas com .NET

## Estrutura da solução

```text
RestApiModeloDDD

1 - API

RestApiModeloDDD.API
 └── Controllers
      ├── ClienteController.cs
      └── ProdutoController.cs

2 - Application

RestApiModeloDDD.Application
 ├── DTOs
 ├── Interfaces
 ├── Mappers
 └── Services

3 - Domain

RestApiModeloDDD.Domain
 ├── Entities
 ├── Interfaces
 │    ├── Repositories
 │    └── Services
 ├── Services
 └── Validations

4 - Infrastructure

RestApiModeloDDD.Infrastructure
 ├── IoC
 └── Data
      ├── Context
      └── Repositories

5 - Tests

RestApiModeloDDD.Tests
```

## Camadas

### API
Responsável pela exposição da aplicação através de endpoints HTTP.

### Application
Camada de orquestração entre apresentação e domínio.

### Domain
Contém as regras de negócio, entidades e contratos da aplicação.

### Infrastructure
Responsável pelo acesso a dados, persistência e injeção de dependência.

### Tests
Projeto dedicado aos testes unitários e validações da aplicação.

## Conceitos abordados

- DDD
- SOLID
- Clean Code
- Injeção de Dependência
- Repository Pattern
- Service Pattern
- AutoMapper
- Entity Framework
- Testes unitários

## Finalidade

Este projeto tem como objetivo servir como modelo de arquitetura para APIs corporativas utilizando .NET, com foco em organização, escalabilidade e manutenção.
```