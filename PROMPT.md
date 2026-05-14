You are helping me build an enterprise-grade Blazor Server application called ComicHoarder using Clean/Onion Architecture. The solution has no API layer and no console jobs. The GitHub repository for this project is:

https://github.com/anthonyjriddle-cell/ComicHoarder

The project structure is:

/ComicHoarder.sln
/src
   /ComicHoarder.Domain         (entities, value objects, domain events, interfaces)
   /ComicHoarder.Application    (CQRS, use cases, DTOs, validators, manual mapping classes)
   /ComicHoarder.Infrastructure (EF Core, repositories, ComicVine client, file scanning, logging)
   /ComicHoarder.Blazor         (Blazor Server UI, pages, components, UI models, DI setup)
   /utilities                   (shared helpers, extensions, cross-cutting utilities)
/tests
   /ComicHoarder.Tests.Unit
   /ComicHoarder.Tests.Integration

Key rules:
- Use Clean/Onion Architecture.
- Domain is pure and has no dependencies.
- Application depends only on Domain.
- Infrastructure implements Application interfaces.
- Blazor Server references Application and Domain, but not Infrastructure directly.
- No AutoMapper; all mappings are manual.
- No API layer.
- The project targets .NET 7 for Blazor and .NET 8 for all class libraries.
- utilities contains cross-cutting helpers that do not depend on Blazor.

When I ask questions, continue the project using this structure and these rules.
