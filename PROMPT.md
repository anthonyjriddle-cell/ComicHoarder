# ComicHoarder — Project Development Prompt

This prompt defines the full architecture, domain rules, naming conventions, workflows, and development expectations for the ComicHoarder application.
Paste this into any new AI session (Copilot, ChatGPT, Claude, etc.) to bring it up to speed and ready to make changes.

---

# 🎯 Project Summary

ComicHoarder is a **Clean Architecture / Onion Architecture** solution for managing a personal comic book collection. It is both a **Blazor Server web app** and a set of **standalone background jobs**, sharing the same Domain/Application/Infrastructure layers.

It integrates with **ComicVine** to import missing Volumes and Issues, downloads comics from external sources, and converts PDFs to CBZ for reading.

The system consists of:

- **Domain Models** (Volume, Issue, Publisher, IssueFormat, Event, Settings, Dashboard projections, etc.)
- **Application Layer** (Use Cases, Repository Interfaces)
- **Infrastructure Layer** (EF Core Repositories, Entity Models, Mappers)
- **Infrastructure.ComicVine Layer** (ComicVine API integration, WebDataService)
- **Shared Layer** (cross-cutting concerns — currently logging setup, used by both the web app and the jobs)
- **UI Layer** (Blazor Server Components + Pages)
- **Jobs** (three independent console/worker apps that run on a schedule or on demand)
- **Tests** (MSTest project covering the Jobs)

All new code should follow the conventions below exactly, since the existing codebase is consistent throughout.

---

# 🧱 Solution Structure

This is the real, current `src/` layout (source files only — `bin`, `obj`, and `.vs` are build/IDE artifacts and are not part of the project):

```
src/
├── ComicHoarder.sln
├── ComicHoarder.Domain/
│   └── Models/
│       ├── Volume.cs
│       ├── Issue.cs
│       ├── IssueFormat.cs
│       ├── Publisher.cs
│       ├── Event.cs
│       ├── Settings.cs
│       └── ComicIssuesToCollectCountByPublisher.cs
│
├── ComicHoarder.Application/
│   ├── Interfaces/
│   │   ├── IVolumeRepository.cs
│   │   ├── IIssueRepository.cs
│   │   ├── IIssueFormatRepository.cs
│   │   ├── IPublisherRepository.cs
│   │   ├── IComicIssuesToCollectCountByPublisherEFCoreRepository.cs
│   │   └── IWebDataService.cs
│   └── UseCases/
│       ├── Volumes/            (Add, Delete, Edit, ViewById, ViewByPublisherAndName + Interfaces/)
│       ├── Issues/              (Add, Delete, Edit, GetAllIssueFormats, ViewById, ViewByVolumeAndName + Interfaces/)
│       ├── Publishers/          (Add, Delete, Edit, ViewById, ViewByName + Interfaces/)
│       ├── ComicVine/           (SearchComicVinePublisher, SearchMissingComicVineIssueByVolume,
│       │                         SearchMissingComicVinePublishers, SearchMissingComicVineVolumesByPublisher + Interfaces/)
│       └── Dashboard/           (GetComicIssuesToCollectCountByPublisher + Interfaces/)
│
├── ComicHoarder.Infrastructure/
│   ├── CHContext.cs                          (EF Core DbContext)
│   ├── DependencyInjection.cs                (DI registration for this layer)
│   ├── IssueEFCoreRepository.cs
│   ├── IssueFormatEFCoreRepository.cs
│   ├── PublisherEFCoreRepository.cs
│   ├── VolumeEFCoreRepository.cs
│   ├── ComicIssuesToCollectCountByPublisherEFCoreRepository.cs
│   ├── Mappers/                               (one mapper per entity, entity <-> domain model)
│   └── Models/                                (16 EF Core entity classes — *Entity.cs)
│
├── ComicHoarder.Infrastructure.ComicVine/
│   ├── WebDataService.cs                      (implements IWebDataService)
│   ├── WebConnection.cs
│   ├── URLBuilder.cs
│   ├── ComicVineMapper.cs
│   ├── JsonDeserializer.cs
│   ├── ListToStringHelper.cs
│   ├── ParseHelper.cs
│   ├── ReprintDetector.cs
│   ├── DependencyInjection.cs
│   ├── Interfaces/                            (IURLBuilder, IWebConnection)
│   └── Models/                                (34 ComicVine* DTO classes mirroring the ComicVine API)
│
├── ComicHoarder.Shared/
│   └── LoggingSetup.cs                        (Serilog setup, shared by the Blazor app and all Jobs)
│
├── ComicHoarder.Blazor/                       (the web UI — Blazor Server)
│   ├── App.razor, Program.cs, _Imports.razor
│   ├── Controls/
│   │   ├── ComicVinePublisherListComponent.razor
│   │   ├── ComicVineVolumeListComponent.razor
│   │   ├── ComicVineIssueListComponent.razor
│   │   ├── PublisherListComponent.razor / EditPublisherComponent.razor
│   │   ├── VolumeListComponent.razor / EditVolumeComponent.razor
│   │   ├── IssueListComponent.razor / EditIssueComponent.razor
│   │   ├── IssueFormatDropdownComponent.razor   (dropdown for Issue.FormatId, see below)
│   │   ├── DeleteConfirmationDialogComponent.razor
│   │   ├── PublisherPieChartComponent.razor
│   │   └── SearchComponent.razor
│   ├── Layout/ (MainLayout.razor, NavMenu.razor)
│   ├── Pages/
│   │   ├── Dashboard/Dashboard.razor
│   │   ├── Publisher/ (AddPublisher, EditPublisher, PublisherList)
│   │   ├── Volume/    (AddVolume, EditVolume, VolumeList)
│   │   └── Issue/     (AddIssue, EditIssue, IssueList)
│   ├── Services/DataTablesInterop.cs
│   ├── Shared/SurveyPrompt.razor
│   └── wwwroot/
│
├── Jobs/
│   ├── ComicVineDBSync/          (syncs local DB against ComicVine; Services/IssueService.cs, VolumeService.cs)
│   ├── GetComicsDownloader/      (scrapes/downloads comics; Services/GetComicsHtmlParser.cs, GetComicsHttpService.cs, GetComicsUrlBuilder.cs)
│   └── PDFToCBZ/                 (converts PDFs to CBZ; Services/PDFToImagesService.cs, ZipService.cs)
│
├── Tests/
│   └── ComicHoarder.Jobs.Tests/  (MSTest; currently covers GetComicsHtmlParser)
│
└── Utility.DBScaffold/           (one-off EF Core scaffolding utility, not part of the running app)
```

---

# 🧱 Architecture Rules

## Clean Architecture Layers

- **Domain**
  - Contains POCO models only
  - No dependencies on other layers

- **Application**
  - Contains Use Cases
  - Contains Interfaces for repositories & services
  - No EF Core, no Blazor, no UI logic

- **Infrastructure**
  - Contains EF Core repositories and entity models
  - Implements Application interfaces
  - Contains Mappers that convert between EF entities and Domain models

- **Infrastructure.ComicVine**
  - All ComicVine API access lives here, isolated from the rest of Infrastructure
  - Implements `IWebDataService` from the Application layer
  - Returns Domain models, never ComicVine DTOs, across the layer boundary

- **Shared**
  - Cross-cutting concerns with no business logic (currently: logging setup)
  - Referenced by the Blazor app and by all three Jobs

- **UI (Blazor)**
  - Pages + Controls
  - Injects Use Cases
  - No direct repository access

- **Jobs**
  - Independent executables, each with its own `Program.cs`
  - Use the same Application/Infrastructure/Domain layers as the web app
  - No web/UI dependency

---

# 📦 Domain Models

> **Note:** Only `Volume` and `Issue` have been directly verified against the current source in this conversation. `Publisher`, `Event`, `Settings`, and `ComicIssuesToCollectCountByPublisher` exist (confirmed by file listing) but their exact fields have not been individually re-verified — treat their shape below as a best-effort placeholder and check the actual file before relying on field names.

## Volume

```csharp
public class Volume
{
    public int Id { get; set; }
    public int PublisherId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public bool Collectable { get; set; }
    public bool Enabled { get; set; }
    public DateTime? DateLastUpdated { get; set; }
}
```

## Issue

```csharp
public class Issue
{
    public int Id { get; set; }
    public int VolumeId { get; set; }
    public string? Name { get; set; }
    public float IssueNumber { get; set; }
    public int PublishMonth { get; set; }
    public int PublishYear { get; set; }
    public bool Collected { get; set; }
    public bool Enabled { get; set; }
    public string? IssueNumberSuffix { get; set; }
    public int? FormatId { get; set; }      // NULLABLE — confirmed. An issue may have no format assigned.
    public bool Reprint { get; set; }
    public DateTime? DateAdded { get; set; }
    public string? Summary { get; set; }
    public DateTime? DateLastUpdated { get; set; }
    public DateTime? CoverDate { get; set; }
}
```

## IssueFormat

A lookup table referenced by `Issue.FormatId`.

```csharp
public class IssueFormat
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool Enabled { get; set; }
}
```

Display/selection rule (used by `IssueFormatDropdownComponent`, see UI section below):
- Only formats where `Enabled == true` are shown in pickers by default.
- **Exception:** if an `Issue` already has a `FormatId` pointing at a *disabled* format, that format must still appear in the dropdown for that issue (so existing data doesn't silently disappear), but it should not appear for any issue that doesn't already have it selected.

## Publisher, Event, Settings, ComicIssuesToCollectCountByPublisher

These models exist in `Domain/Models/` but have not been re-verified field-by-field in this document. `ComicIssuesToCollectCountByPublisher` backs the Dashboard feature (see below) and is a read projection rather than a persisted entity, based on its naming and its dedicated EF Core repository/mapper pair.

---

# 🔌 Repository Interfaces

The Application layer defines one repository interface per aggregate, implemented by an EF Core repository in Infrastructure:

- `IVolumeRepository`
- `IIssueRepository`
- `IIssueFormatRepository`
- `IPublisherRepository`
- `IComicIssuesToCollectCountByPublisherEFCoreRepository` (backs the Dashboard read projection)

Pattern (verified shape from `IVolumeRepository`-style usage elsewhere in the prior version of this file — confirm exact members against the source file when adding new methods):

```csharp
public interface IVolumeRepository
{
    Task AddVolumeAsync(Volume volume);
    Task<List<int>> GetAllVolumeId();
    // Edit/Delete/ViewById/ViewByName methods also exist, following the same async naming pattern,
    // since the Application layer has matching Edit/Delete/View use cases for Volumes, Issues, and Publishers.
}
```

Apply the same shape consistently for `IIssueRepository`, `IPublisherRepository`, and `IIssueFormatRepository` — each should expose whatever the corresponding Use Cases in `UseCases/{Entity}/` actually call.

---

# 🌐 ComicVine Integration

`IWebDataService` (Application layer interface, implemented by `WebDataService` in `Infrastructure.ComicVine`) must expose:

```csharp
IEnumerable<Volume> GetVolumesFromPublisher(int publisherId);
IEnumerable<Issue> GetIssuesFromVolume(int volumeId);
```

Returned models must be mapped to Domain models (via `ComicVineMapper`) before crossing the layer boundary — UI and Application code must never see a `ComicVine*` DTO directly.

ComicVine link patterns:

```
Publisher: https://comicvine.gamespot.com/publisher/4010-{Publisher.Id}/
Volume:    https://comicvine.gamespot.com/volume/4050-{Volume.Id}/
Issue:     https://comicvine.gamespot.com/issue/4000-{Issue.Id}/
```

```html
<a href="https://comicvine.gamespot.com/publisher/4010-@(Publisher.Id)/"
   target="_blank"
   rel="noopener noreferrer">
```

---

# 🧠 Use Case Rules

## Naming & Location

- Volume use cases live in `Application.UseCases.Volumes`
- Issue use cases live in `Application.UseCases.Issues`
- Publisher use cases live in `Application.UseCases.Publishers`
- ComicVine search use cases live in `Application.UseCases.ComicVine`
- Dashboard read use cases live in `Application.UseCases.Dashboard`

Each entity (Volume, Issue, Publisher) follows the same CRUD-style set:

```
Add{Entity}UseCase
Delete{Entity}UseCase
Edit{Entity}UseCase
View{Entity}ByIdUseCase
View{Entity}sBy<criteria>UseCase     (e.g. ViewVolumesByPublisherAndName, ViewIssuesByVolumeAndName, ViewPublishersByName)
```

Issue additionally has:

```
GetAllIssueFormatsUseCase   (returns the full IssueFormat lookup list for the dropdown)
```

ComicVine has:

```
SearchComicVinePublisherUseCase
SearchMissingComicVinePublishersUseCase
SearchMissingComicVineVolumesByPublisherUseCase
SearchMissingComicVineIssueByVolumeUseCase
```

Dashboard has:

```
GetComicIssuesToCollectCountByPublisherUseCase
```

Every use case has a matching `I{UseCaseName}` interface in an adjacent `Interfaces/` folder, and is injected into Blazor components by interface, never by concrete class.

## Examples

### AddVolumeUseCase

```csharp
public class AddVolumeUseCase : IAddVolumeUseCase
{
    private readonly IVolumeRepository volumeRepository;

    public AddVolumeUseCase(IVolumeRepository volumeRepository)
    {
        this.volumeRepository = volumeRepository;
    }

    public async Task ExecuteAsync(Volume volume)
    {
        await volumeRepository.AddVolumeAsync(volume);
    }
}
```

### AddIssueUseCase

```csharp
public class AddIssueUseCase : IAddIssueUseCase
{
    private readonly IIssueRepository issueRepository;

    public AddIssueUseCase(IIssueRepository issueRepository)
    {
        this.issueRepository = issueRepository;
    }

    public async Task ExecuteAsync(Issue issue)
    {
        await issueRepository.AddIssueAsync(issue);
    }
}
```

### GetAllIssueFormatsUseCase

```csharp
public class GetAllIssueFormatsUseCase : IGetAllIssueFormatsUseCase
{
    private readonly IIssueFormatRepository issueFormatRepository;

    public GetAllIssueFormatsUseCase(IIssueFormatRepository issueFormatRepository)
    {
        this.issueFormatRepository = issueFormatRepository;
    }

    public async Task<IEnumerable<IssueFormat>> ExecuteAsync()
    {
        return await issueFormatRepository.GetAllAsync();
    }
}
```

### SearchMissingComicVineVolumesByPublisherUseCase

```csharp
public class SearchMissingComicVineVolumesByPublisherUseCase : ISearchMissingComicVineVolumesByPublisherUseCase
{
    private readonly IWebDataService webDataService;
    private readonly IVolumeRepository volumeRepository;

    public async Task<IEnumerable<Volume>> ExecuteAsync(int publisherId)
    {
        var comicVineVolumes = webDataService.GetVolumesFromPublisher(publisherId)
            ?? new List<Volume>();

        var localIds = await volumeRepository.GetAllVolumeId();

        return comicVineVolumes.Where(v => !localIds.Contains(v.Id));
    }
}
```

### SearchMissingComicVineIssueByVolumeUseCase

```csharp
public class SearchMissingComicVineIssueByVolumeUseCase : ISearchMissingComicVineIssueByVolumeUseCase
{
    private readonly IWebDataService webDataService;
    private readonly IIssueRepository issueRepository;

    public async Task<IEnumerable<Issue>> ExecuteAsync(int volumeId)
    {
        var comicVineIssues = webDataService.GetIssuesFromVolume(volumeId)
            ?? new List<Issue>();

        var localIds = await issueRepository.GetAllIssueIds();

        return comicVineIssues.Where(i => !localIds.Contains(i.Id));
    }
}
```

---

# 🖥️ UI Layer (Blazor)

## Routing conventions

```
/AddVolume/{pubId}
/AddIssue/{volumeId}
/AddPublisher
/Volumes/{PublisherId}
/Issues/{VolumeId}
/Publishers
```

- After adding a Volume → navigate to `/Volumes/{PublisherId}`
- After adding an Issue → navigate to `/Issues/{VolumeId}`
- Publisher has its own full CRUD page set (`AddPublisher`, `EditPublisher`, `PublisherList`) under `Pages/Publisher/`, following the same pattern as Volume and Issue.
- There is also a `Pages/Dashboard/Dashboard.razor`, which is not part of the original Add/Edit/Delete flow — it's a reporting page backed by `GetComicIssuesToCollectCountByPublisherUseCase`.

## Edit Components (Controls/)

`Edit{Entity}Component.razor` (e.g. `EditIssueComponent`, `EditVolumeComponent`, `EditPublisherComponent`) follow this pattern:

- Receive the entity as a `[Parameter]`
- Use `<EditForm>` + `<DataAnnotationsValidator>` + per-field `<ValidationMessage>`
- Do **not** inject the corresponding `Edit{Entity}UseCase` directly inside the component. Instead they expose `[Parameter] public EventCallback<{Entity}?> OnUpdate { get; set; }` and call `OnUpdate.InvokeAsync(entity)` on submit, delegating the actual use-case call (and any in-memory list update) to the parent page/list component. This lets a parent component update a single row in a displayed list without a full page refresh.
- Use `@bind-Value` (capital V) for `InputText`/`InputNumber`/`InputDate`, and `@bind-Value` for `InputCheckbox` as well — not the lowercase `@bind-value`, which silently fails to bind in Blazor.

## IssueFormatDropdownComponent

A self-contained dropdown for picking an `Issue`'s format, used in place of a raw numeric `FormatId` input.

- Injects its own use case (`IGetAllIssueFormatsUseCase`) and loads the format list on `OnInitializedAsync` — it does not require the parent to pass the list in.
- Two-way bindable via `[Parameter] int? SelectedFormatId` + `[Parameter] EventCallback<int?> SelectedFormatIdChanged`, so it's used as `<IssueFormatDropdownComponent @bind-SelectedFormatId="Issue.FormatId" />`.
- Always renders a blank option (maps to `null`).
- Filters the list to `Enabled == true` OR `Id == SelectedFormatId`, so a disabled format already assigned to the issue stays visible and selectable, but doesn't appear as a choice for other issues.
- Selecting the blank option sets the bound value back to `null`.
- **Must use the `form-select` Bootstrap class, not `form-control`**, on the `<select>` element — `form-control` is for text inputs and makes a `<select>` render without the dropdown arrow/styling.

## ComicVine*ListComponent (Controls/)

- `ComicVineVolumeListComponent`, `ComicVineIssueListComponent`, `ComicVinePublisherListComponent`
- Each injects the matching `SearchMissingComicVine*UseCase` and displays a table of items missing from the local database, with an "Add" action per row that calls the matching `Add{Entity}UseCase` and navigates per the routing conventions above.
- (The previous version of this document described separate `*ListItemComponent` child components per row. The current file listing shows only the list components themselves — confirm against the actual `.razor` markup whether row rendering was inlined or simply isn't separately named before assuming a separate component exists.)

## Other Controls

- `DeleteConfirmationDialogComponent` — generic confirmation dialog, reused across entities.
- `PublisherPieChartComponent` — chart on the Dashboard or Publisher pages showing collection breakdown.
- `SearchComponent` — generic search input, reused across list pages.

## CSS Utilities

```css
.wrap-long-text {
    white-space: normal;
    word-break: break-word;
}

.narrow-col {
    width: 1%;
    white-space: nowrap;
}
```

---

# ⚙️ Jobs

Three independent executables under `Jobs/`, sharing Domain/Application/Infrastructure/Shared with the web app but with no dependency on Blazor:

- **ComicVineDBSync** — syncs the local database against ComicVine via `IssueService` and `VolumeService`, presumably driving the same `SearchMissingComicVine*UseCase`s used by the UI.
- **GetComicsDownloader** — scrapes a source (via `GetComicsHtmlParser` + `GetComicsHttpService` + `GetComicsUrlBuilder`) and downloads comic files; tracks state in `LastDate.txt`.
- **PDFToCBZ** — converts downloaded PDFs into CBZ archives via `PDFToImagesService` and `ZipService`; has environment-specific appsettings (`appsettings.Linux.json`, `appsettings.Windows.json`, `appsettings.Test.json`).

Each Job has its own `Program.cs` and `appsettings.json`, and uses `ComicHoarder.Shared.LoggingSetup` for consistent logging configuration.

`Tests/ComicHoarder.Jobs.Tests` is an MSTest project; currently it covers `GetComicsHtmlParser` against sample HTML fixtures in `TestFiles/`.

---

# 🛠️ Development Rules

- All new code must follow the patterns defined above.
- All Issue functionality must mirror Volume functionality 1:1 (and, where applicable, Publisher too — all three entities now follow the same Add/Edit/Delete/View shape).
- No repository or EF Core logic in UI or Use Cases.
- No UI logic in Use Cases.
- No ComicVine logic in UI or Infrastructure repositories — it's isolated to `Infrastructure.ComicVine`.
- All new components must follow existing naming conventions (`{Verb}{Entity}Component`, `{Verb}{Entity}UseCase`, `I{Verb}{Entity}UseCase`).
- All new use cases must follow existing async patterns (`ExecuteAsync`, injected repository/service dependencies, one responsibility per use case).
- All new pages must follow existing routing patterns.
- Nullable reference/value types matter here — e.g. `Issue.FormatId` is `int?` specifically because an issue can have no format assigned; don't assume non-nullable just because an older field looks similar.
- Bootstrap `<select>` elements need `form-select`, not `form-control`.

---

# 📝 Maintenance Note

This file should be kept in sync with the actual codebase. When the structure changes meaningfully (new project, new top-level feature, new layer), update this file in the same change. If you're an AI picking this up: where this document says something was "inferred" or "not directly verified," check the real source file before treating it as fact, especially for exact method signatures on repository interfaces.

---

# ✔️ End of Prompt

Paste this entire block into a new AI session to bootstrap development context for ComicHoarder.