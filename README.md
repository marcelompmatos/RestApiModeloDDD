# RestApiModeloDDD

# RestApiModeloDDD

API desenvolvida em .NET 9 com foco em arquitetura corporativa, aplicando conceitos de Domain-Driven Design (DDD), separação por camadas e boas práticas utilizadas em sistemas reais.

O objetivo do projeto é demonstrar uma estrutura escalável e organizada para construção de APIs empresariais, priorizando:

- manutenção
- desacoplamento
- observabilidade
- escalabilidade
- clareza de responsabilidades
- testabilidade
- arquitetura enterprise

---

## Arquitetura aplicada

Este projeto foi estruturado com base em:

- Domain-Driven Design (DDD)
- SOLID
- Clean Code
- Clean Architecture
- Repository Pattern
- Service Layer
- Dependency Injection
- DTO Pattern
- Object Mapping
- Middleware Pipeline
- Structured Logging
- Observabilidade
- Exception Handling Global
- Testes automatizados

---

## Tecnologias

![.NET](https://img.shields.io/badge/.NET-9.0-purple)
![EF Core](https://img.shields.io/badge/Entity%20Framework-Core-blue)
![Autofac](https://img.shields.io/badge/Autofac-IoC-green)
![AutoMapper](https://img.shields.io/badge/AutoMapper-Mapping-orange)
![Serilog](https://img.shields.io/badge/Serilog-Logging-green)
![xUnit](https://img.shields.io/badge/xUnit-Tests-red)
![SQL Server](https://img.shields.io/badge/SQL-Server-darkblue)

### Stack utilizada

- C#
- ASP.NET Core
- Entity Framework Core
- SQL Server
- AutoMapper
- Autofac
- Serilog
- xUnit
- Middleware Global
- Structured Logging

---

## Estrutura da solução

```text

RestApiModeloDDD
│
├── RestApiModeloDDD.API
│   ├── Controllers
│   │   ├── ClienteController.cs
│   │   ├── ProdutoController.cs
│   │   └── PedidoController.cs
│   │
│   ├── Middlewares
│   │   └── ExceptionMiddleware.cs
│   │
│   ├── Logging
│   │   └── SerilogConfiguration.cs
│   │
│   ├── Configurations
│   │   ├── SwaggerConfiguration.cs
│   │   └── AutoMapperConfiguration.cs
│   │
│   ├── Program.cs
│   ├── Startup.cs
│   └── appsettings.json
│
├── RestApiModeloDDD.Application
│   ├── DTOs
│   │   ├── ClienteDto.cs
│   │   ├── ProdutoDto.cs
│   │   ├── PedidoDto.cs
│   │   └── ItemPedidoDto.cs
│   │
│   ├── Interfaces
│   │   ├── IApplicationServiceBase.cs
│   │   ├── IApplicationServiceCliente.cs
│   │   ├── IApplicationServiceProduto.cs
│   │   └── IApplicationServicePedido.cs
│   │
│   ├── Mappers
│   │   ├── DtoToModelMappingCliente.cs
│   │   ├── DtoToModelMappingProduto.cs
│   │   ├── DtoToModelMappingPedido.cs
│   │   ├── DtoToModelMappingItemPedido.cs
│   │   ├── ModelToDtoMappingCliente.cs
│   │   ├── ModelToDtoMappingProduto.cs
│   │   ├── ModelToDtoMappingPedido.cs
│   │   └── ModelToDtoMappingItemPedido.cs
│   │
│   └── Services
│       ├── ApplicationServiceBase.cs
│       ├── ApplicationServiceCliente.cs
│       ├── ApplicationServiceProduto.cs
│       └── ApplicationServicePedido.cs
│
├── RestApiModeloDDD.Domain
│   ├── Entities
│   │   ├── Base.cs
│   │   ├── Cliente.cs
│   │   ├── Produto.cs
│   │   ├── Pedido.cs
│   │   └── ItemPedido.cs
│   │
│   ├── Interfaces
│   │   ├── Services
│   │   │   ├── IServiceBase.cs
│   │   │   ├── IServiceCliente.cs
│   │   │   ├── IServiceProduto.cs
│   │   │   └── IServicePedido.cs
│   │   │
│   │   └── Repositories
│   │       ├── IRepositoryBase.cs
│   │       ├── IRepositoryCliente.cs
│   │       ├── IRepositoryProduto.cs
│   │       ├── IRepositoryPedido.cs
│   │       └── IRepositoryItemPedido.cs
│   │
│   ├── Services
│   │   ├── ServiceBase.cs
│   │   ├── ServiceCliente.cs
│   │   ├── ServiceProduto.cs
│   │   └── ServicePedido.cs
│   │
│   └── Validations
│       ├── ClienteValidation.cs
│       ├── ProdutoValidation.cs
│       └── PedidoValidation.cs
│
├── RestApiModeloDDD.Infrastructure
│   ├── Data
│   │   ├── Context
│   │   │   └── SqlContext.cs
│   │   │
│   │   ├── Repository
│   │   │   ├── RepositoryBase.cs
│   │   │   ├── RepositoryCliente.cs
│   │   │   ├── RepositoryProduto.cs
│   │   │   ├── RepositoryPedido.cs
│   │   │   └── RepositoryItemPedido.cs
│   │   │
│   │   └──
│   │
│   └── IoC
│       ├── ConfigurationIOC.cs
│       └── ModuleIOC.cs
│
└── RestApiModeloDDD.Tests
    ├── Application
    │   ├── ClienteTests.cs
    │   ├── ProdutoTests.cs
    │   └── PedidoTests.cs
    │
    ├── Domain
    │   ├── ServiceClienteTests.cs
    │   ├── ServiceProdutoTests.cs
    │   └── ServicePedidoTests.cs
    │
    └── Repository
        ├── RepositoryClienteTests.cs
        ├── RepositoryProdutoTests.cs
        └── RepositoryPedidoTests.cs

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

Conversão entre entidades e DTOs utilizando AutoMapper..

---

## Testes

Validação de regras utilizando xUnit.

---

---

## Fluxo da aplicação

A comunicação entre as camadas ocorre da seguinte forma:

```text
Controller
↓
Application Service
↓
Domain Service
↓
Repository
↓
DbContext
↓
SQL Server
```

Esse fluxo garante separação de responsabilidades, desacoplamento e manutenção facilitada.

---

## Explicação das principais classes

### Controllers

Responsáveis por receber requisições HTTP e delegar o processamento para a camada de aplicação.

Exemplos:

- ClienteController
- ProdutoController

Aplicado:

- API
- DTO
- Service Layer
- SOLID

---

### DTOs

Objetos de transferência de dados entre API e aplicação.

Evita expor entidades de domínio diretamente.

Exemplos:

- ClienteDTO
- ProdutoDTO

Aplicado:

- DTO
- Clean Code
- Encapsulamento

---

### Application Services

Responsáveis por orquestrar chamadas entre API e domínio.

Exemplos:

- ApplicationServiceCliente

Aplicado:

- Service Layer
- DDD
- SOLID

---

### Entities

Representam entidades do domínio.

Exemplos:

- Cliente
- Produto

Aplicado:

- DDD
- Entidades
- Regras de negócio

---

### Domain Services

Centralizam regras de negócio.

Exemplos:

- ServiceCliente

Aplicado:

- Service Pattern
- DDD

---

### Repositories

Abstraem acesso aos dados.

Interfaces:

- IClienteRepository

Implementações:

- RepositoryCliente

Aplicado:

- Repository Pattern
- SOLID

---

### Mappers

Responsáveis pela conversão entre DTOs e entidades do domínio, garantindo desacoplamento entre as camadas da aplicação.

Utilizando AutoMapper para automatizar o mapeamento de objetos.

Aplicado:

- Object Mapping
- DTO Pattern
- Separação entre camadas
- Desacoplamento de responsabilidades
- Organização da arquitetura

### IoC — Inversão de Controle

A classe `ConfigurationIOC` é responsável por centralizar o registro das dependências da aplicação.

Nela são configuradas as associações entre interfaces e implementações concretas, permitindo que o container gerencie automaticamente a criação e o ciclo de vida dos objetos.

### Responsabilidade

Realizar o mapeamento das dependências entre as camadas da arquitetura:

- Application
- Domain
- Infrastructure

### Como funciona

O método `Load(ContainerBuilder builder)` registra cada serviço no container do Autofac.

Exemplo:

- `ApplicationServiceCliente` → `IApplicationServiceCliente`
- `ServiceCliente` → `IServiceCliente`
- `RepositoryCliente` → `IRepositoryCliente`

Quando uma classe precisa de uma interface no construtor, o container injeta automaticamente sua implementação registrada.

### AutoMapper

Também realiza o registro dos perfis do :contentReference[oaicite:1]{index=1}:

- DTO para Entity
- Entity para DTO

Permitindo conversão automática entre objetos de diferentes camadas.

### Fluxo

Controller  
↓  
ApplicationService  
↓  
Service  
↓  
Repository  
↓  
DbContext

Cada camada recebe suas dependências via injeção.

### Conceitos aplicados

- Dependency Injection
- Inversion of Control
- SOLID
- Separation of Concerns
- Clean Architecture
- DDD


### Context

Classe responsável pela configuração de persistência.

Exemplo:

- ContextBase

Utilizando :contentReference[oaicite:3]{index=3}.

Aplicado:

- ORM
- Persistência

---
## Decisão Arquitetural — Unit of Work

Embora o padrão Unit of Work seja amplamente utilizado em arquiteturas corporativas, neste projeto sua implementação foi propositalmente omitida.

Isso ocorre porque o Entity Framework Core já atua como Unit of Work através do `DbContext`, sendo responsável por:

- Change Tracking
- Transaction Management
- Commit de operações
- Controle de consistência

Assim, optou-se por utilizar diretamente o `DbContext`, reduzindo complexidade e evitando sobreposição de padrões já fornecidos pela infraestrutura.

### Tests

Validação automatizada das regras.

Utilizando xunit.

Aplicado:

- Testes unitários
- Qualidade de código

---

## Benefícios da arquitetura

A estrutura adotada permite:

- Separação clara entre responsabilidades
- Facilidade de manutenção
- Escalabilidade
- Reutilização de componentes
- Testabilidade
- Organização em projetos reais
- Evolução para microsserviços
- Integração com cloud

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

Projeto desenvolvido para estudo prático e aprofundamento em arquitetura de software com .NET, com foco em boas práticas, escalabilidade e organização de código.