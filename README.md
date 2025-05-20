# Ledger System

A lightweight and extensible ledger system that allows users to record deposits and withdrawals, view current balances, and track transaction history.
This project follows *Clean Architecture* principles and implements the *Command Pattern* to keep business logic clean, maintainable, and testable. 
Persistence is handled via an *in-memory repository* for quick development and testing.

---

## Architecture Overview

- *Domain Layer*: Core business logic (LedgerModel, TransactionRecord)
- *Application Layer*: Use cases and orchestration (LedgerService, DepositCommand, WithdrawCommand)
- *Infrastructure Layer*: Repository implementation (LedgerRepository)
- *API Layer*: HTTP endpoints to interact with the system

---

## Features

- Create a ledger
- Deposit money into a ledger
- Withdraw money from a ledger
- View the current balance
- View transaction history
- Fully testable with in-memory data
- Structured for future expansion (real DB, eventing, etc.)

---

## Tech Stack

- [.NET 6+](https://dotnet.microsoft.com/en-us/download)
- ASP.NET Core Web API
- C#
- In-memory data store (no database needed)
- Follows SOLID, Clean Architecture, and Command Pattern principles

---

## Getting Started

### Run the API

```bash
dotnet run --project Ledger.Api
```

### API Endpoints

| **Method** | **Route**                               | **Description**             |
|------------|-----------------------------------------|-----------------------------|
| `POST`     | `/api/ledger/`                          | Create a new ledger         |
| `POST`     | `/api/ledger/{ledgerId}/deposit`        | Deposit funds into a ledger |
| `POST`     | `/api/ledger/{ledgerId}/withdraw`       | Withdraw funds from a ledger |
| `POST`     | `/api/ledger/{ledgerId}/revert`         | Revert last operation for the ledger |
| `GET`      | `/api/ledger/{ledgerId}/balance`        | Get the current balance of the ledger |
| `GET`      | `/api/ledger/{ledgerId}/transactions`  | Get transaction history for a ledger |
| `GET`      | `/api/ledger/{ledgerId}/balance/{date}`| Get the balance for a specific date


### Start from Creation of a Ledger

1. POST /api/ledger/ and then use ledger id for other operations
2. POST /api/ledger/{ledgerId}/deposit
{
  "ledgerId": "b349f1ef-bc88-4a21-9f99-0dc41d37e06c",
  "amount": 100.00
}

## Design Principles & Patterns
- Clean Architecture: Separation of concerns by layers
- Command Pattern: Encapsulates business operations like deposit and withdraw
- Repository Pattern: Decouples persistence logic from business logic
- Inversion of Control: Easily testable services with dependency injection
