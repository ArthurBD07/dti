# Lead Management – Desafio DTI

Aplicação desenvolvida para o processo seletivo de estágio na DTI.

O sistema é composto por uma **API REST em .NET** e uma **SPA em React**, e simula o fluxo de gestão de leads de serviços: o usuário visualiza os leads convidados (*Invited*), pode **aceitar** ou **recusar** cada um, e acompanha os leads aceitos na aba *Accepted*.

Quando a aplicação é executada pela primeira vez, o backend cria o banco de dados no **SQL Server** e popula automaticamente dois leads de exemplo (Bill e Craig), ambos na aba **Invited**. A aba **Accepted** começa vazia, como pedido no desafio.

---

## 🎯 Objetivo do Projeto

- Expor uma **API .NET Core 9** que:
  - Liste leads com status `invited` e `accepted`
  - Permita aceitar ou recusar leads
  - Persista os dados em um banco **SQL Server** usando **Entity Framework Core**
- Implementar uma **SPA em React** que:
  - Mostre duas abas: **Invited** e **Accepted**
  - Permita aceitar ou recusar leads via interface
  - Reflita em tela as mudanças de status retornadas pela API
- Incluir uma **camada de testes unitários** para a API.

---

## 🛠 Tecnologias Principais

- **Backend**: .NET 8, ASP.NET Core Minimal API, Entity Framework Core, SQL Server  
- **Frontend**: React + Vite, JavaScript (ES6), CSS  
- **Testes**: xUnit

---

## 🚀 Como executar o projeto

### 1. Pré-requisitos

- **.NET SDK 8.0 ou superior** instalado  
- **Node.js 18+** (com `npm`)  
- **SQL Server** (pode ser LocalDB ou SQL Server Express/Developer) em execução

A conexão padrão da API usa:

```text
Server=localhost;Database=LeadManagerDb;Trusted_Connection=True;TrustServerCertificate=True;
