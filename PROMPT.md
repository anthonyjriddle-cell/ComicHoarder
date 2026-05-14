You are helping me build an enterprise-grade Blazor Server application called ComicHoarder using Clean/Onion Architecture. The solution has no API layer and no console jobs. The GitHub repository for this project is:

https://github.com/anthonyjriddle-cell/ComicHoarder

The project structure is:

/ComicHoarder.sln
/src
   /ComicHoarder.Domain
       (entities, value objects, domain events, pure business logic)
   /ComicHoarder.Application
       (CQRS, use cases, DTOs, validators, manual mapping classes, application interfaces)
   /ComicHoarder.Infrastructure
       (EF Core DbContext, EF entities, Fluent configs, repositories, file scanning, logging)
   /ComicHoarder.Integrations.ComicVine
       (ComicVine API client, DTOs, retry logic, rate limiting, external service integration)
   /utilities
       (shared helpers, extensions, cross-cutting utilities)
   /Utility.DBScaffold
       (EF Core scaffold-only project; not referenced, not built, used only to generate raw models)
/tests
   /ComicHoarder.Tests.Unit
   /ComicHoarder.Tests.Integration

Key architectural rules:
- Use Clean/Onion Architecture.
- Domain is pure and has no dependencies.
- Application depends only on Domain and contains all use cases.
- Infrastructure depends on Application and Domain and implements Application interfaces.
- Integrations.ComicVine depends on Application and Domain and implements IComicVineClient.
- Blazor references Application and Domain only; it never references Infrastructure or Integrations.
- No AutoMapper; all mappings are manual.
- No API layer.
- The project targets .NET 7 for Blazor and .NET 8 for all class libraries.
- utilities contains cross-cutting helpers that do not depend on Blazor.

Database-first workflow rules:
- Utility.DBScaffold is used ONLY for EF Core scaffolding.
- Scaffolded DbContext and entity classes are copied into Infrastructure/Persistence.
- Utility.DBScaffold is not built, not referenced, and not used at runtime.
- Infrastructure contains the real DbContext, EF entities, Fluent configurations, and migrations.

EF Core package rules:
- Infrastructure uses: Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.SqlServer,
  Microsoft.EntityFrameworkCore.Relational, and Microsoft.EntityFrameworkCore.Design (PrivateAssets=All).
- Utility.DBScaffold uses: Microsoft.EntityFrameworkCore.Tools and Microsoft.EntityFrameworkCore.SqlServer.
- No other project references EF Core.

When I ask questions, continue the project using this structure and these rules.
