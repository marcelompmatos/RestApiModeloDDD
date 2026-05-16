# RestApiModeloDDD

API desenvolvida em .NET 9 com foco em arquitetura corporativa, aplicando conceitos de Domain-Driven Design (DDD), separação por camadas e boas práticas utilizadas em sistemas reais.

O objetivo do projeto é demonstrar uma estrutura escalável e organizada para construção de APIs empresariais, priorizando manutenção, desacoplamento e clareza de responsabilidades.

---

## Arquitetura aplicada

Este projeto foi estruturado com base em:

- Domain-Driven Design (DDD)
- SOLID
- Clean Code
- Repository Pattern
- Service Layer
- Dependency Injection
- DTO
- Mapeamento entre camadas
- Testes automatizados

---

## Tecnologias

![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![EF Core](https://img.shields.io/badge/Entity%20Framework-Core-blue)
![Autofac](https://img.shields.io/badge/Autofac-IoC-green)
![AutoMapper](https://img.shields.io/badge/AutoMapper-Mapping-orange)
![xUnit](https://img.shields.io/badge/xUnit-Tests-red)
![SQL Server](https://img.shields.io/badge/SQL-Server-darkblue)

### Stack utilizada

- C#
- ASP.NET Core
- Entity Framework Core
- SQL Server
- AutoMapper
- Autofac
- xUnit

---

## Estrutura da solução

```text
RestApiModeloDDD
│
├── RestApiModeloDDD.API
│   └── Controllers
│
├── RestApiModeloDDD.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Mappers
│   └── Services
│
├── RestApiModeloDDD.Domain
│   ├── Entities
│   ├── Interfaces
│   ├── Services
│   └── Validations
│
├── RestApiModeloDDD.Infrastructure
│   ├── Data
│   └── IoC
│
└── RestApiModeloDDD.Tests
```

---

## Responsabilidade das camadas

### API

Camada responsável pela exposição da aplicação por meio de endpoints HTTP.

### Application

Camada de aplicação responsável pela orquestração entre interface e domínio.

### Domain

Contém entidades, contratos, validações e regras de negócio.

### Infrastructure

Implementa persistência, acesso a dados e configuração de injeção de dependência.

### Tests

Contém testes automatizados para validação do comportamento da aplicação.

---

## Conceitos aplicados no projeto

## DDD

Separação das responsabilidades conforme domínio de negócio.

Aplicado em:

- Domain
- Application
- Infrastructure
- API

---

## Repository Pattern

Responsável por abstrair o acesso aos dados.

Exemplos:

- IClienteRepository
- RepositoryCliente

---

## Service Layer

Camada de centralização das regras de negócio.

Exemplos:

- ServiceCliente
- ApplicationServiceCliente

---

## DTO

Transferência de dados entre API e aplicação.

Exemplos:

- ClienteDTO
- ProdutoDTO

---

## Dependency Injection

Registro e resolução de dependências.

Aplicado em:

- Infrastructure/IoC

---

## Mapeamento

Conversão entre entidades e DTOs utilizando :contentReference[oaicite:3]{index=3}.

---

## Testes

Validação de regras utilizando :contentReference[oaicite:4]{index=4}.

---

## Objetivo do projeto

Este projeto foi desenvolvido com foco em estudo de arquitetura corporativa, visando aprofundamento em:

- APIs escaláveis
- Organização de código
- Separação de responsabilidades
- Arquitetura limpa
- Reutilização
- Manutenção
- Padrões de projeto

---

## Próximos passos

Evoluções previstas:

- autenticação JWT
- logs estruturados
- versionamento de API
- dockerização
- integração com cloud
- CI/CD
- observabilidade

---

## Autor

Projeto desenvolvido para estudo prático e aprofundamento em arquitetura de software com :contentReference[oaicite:5]{index=5}.