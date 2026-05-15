# ComicHoarder — Clean Architecture Project Context

You are helping me build an enterprise‑grade Blazor Server application called **ComicHoarder**.  
The solution uses **Clean Architecture** with the following layers and conventions.

---

## 🧩 1. Domain Layer

**Purpose:** Pure business logic.

**Rules:**
- Contains only **entities**, **value objects**, and **domain rules**
- No EF Core, no Blazor, no Infrastructure dependencies
- Domain models use **lowercase field names** (e.g., `id`, `name`, `description`)
- Domain models represent business concepts:
  - Publisher
  - Volume
  - Issue
  - Event
  - Settings

---

## 🧩 2. Application Layer

**Purpose:** Orchestrates use cases and defines system behavior.

**Contains:**
- Use case **interfaces**
- Use case **implementations**
- Repository **interfaces**
- DTOs
- Validators

**Structure example:**

ComicHoarder.Application
└── UseCases
└── Publishers
├── Interfaces
│     IAddPublisherUseCase.cs
│     IEditPublisherUseCase.cs
│     IDeletePublisherUseCase.cs
│     IViewPublisherByIdUseCase.cs
│     IViewPublishersByNameUseCase.cs
├── AddPublisherUseCase.cs
├── EditPublisherUseCase.cs
├── DeletePublisherUseCase.cs
├── ViewPublisherByIdUseCase.cs
└── ViewPublishersByNameUseCase.cs


**Rules:**
- Use case interfaces **belong in the Application layer**
- Use cases call repository interfaces
- Use cases return **Domain models**, not EF models
- No Infrastructure or Blazor references

---

## 🧩 3. Infrastructure Layer

**Purpose:** Data access, EF Core, external services.

**Contains:**
- EF Core DbContext
- EF Core models
- Repository implementations
- Mappers (Domain ↔ EF)

**Naming conventions:**
- Table‑backed EF models: `PublisherEntity`, `VolumeEntity`, etc.
- View‑backed EF models: `PublisherDetailsViewEntity`, etc.

**Mapping rules:**
- Mappers are **static**, **pure**, and **synchronous**
- Domain → Data maps lowercase → PascalCase
- Data → Domain maps PascalCase → lowercase
- Missing EF fields map to defaults

**Repository rules:**
- Repositories return **Domain models**
- EF async only at query boundaries (`ToListAsync`, etc.)
- No async inside LINQ
- No async mappers

---

## 🧩 4. Blazor UI Layer

**Purpose:** Presentation layer.

**Rules:**
- Blazor pages call **use cases**, not repositories
- UI uses **Domain models only**
- Feature‑based pages (e.g., `/publishers`, `/publishers/add`)

**Example injection:**

@inject IViewPublishersByNameUseCase ViewPublishersByName

Example usage:

publishers = await ViewPublishersByName.ExecuteAsync(searchText);

5. Repository Pattern
Correct pattern:

var data = await _context.Publishers.ToListAsync();
return data.Select(PublisherDataMapper.ToDomain).ToList();


EF Query Rules:

Use Contains() for case‑insensitive search (SQL Server default)

No async inside LINQ

No async mappers

6. What I Want From You
When I ask for code, generate:

Use case implementations

Repository implementations

Mappers

EF models

Blazor pages

DI configuration

Folder structures

Naming conventions

UI components

Navigation

Validation

Anything needed to continue building ComicHoarder

Follow the architecture above exactly.

7. Project Goal
Build a complete comic‑collection management system with:

Publisher list/search/edit/delete

Volume list/search/edit/delete

Issue list/search/edit/delete

Event and reading‑order support

Clean Architecture separation

Blazor Server UI

EF Core SQL Server backend