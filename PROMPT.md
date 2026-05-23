# ComicHoarder — Project Development Prompt  
This prompt defines the full architecture, domain rules, naming conventions, workflows, and development expectations for the ComicHoarder application.  
Paste this into any new Copilot/ChatGPT session to continue development seamlessly.

---

# 🎯 Project Summary  
ComicHoarder is a **Clean Architecture / Onion Architecture** Blazor Server application for managing comic book collections.  
It integrates with **ComicVine** to import missing Volumes and Issues.

The system consists of:

- **Domain Models** (Volume, Issue, Publisher, etc.)
- **Application Layer** (Use Cases, Interfaces)
- **Infrastructure Layer** (Repositories, EF Core)
- **UI Layer** (Blazor Components + Pages)
- **ComicVine Integration Layer** (WebDataService)

All code generated must follow these conventions exactly.

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
  - Contains EF Core repositories  
  - Implements Application interfaces  

- **UI (Blazor)**  
  - Pages + Components  
  - Injects Use Cases  
  - No direct repository access  

- **Integrations**  
  - ComicVine API access  
  - WebDataService returns domain models  

---

# 📦 Domain Models

## Volume
```
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
```
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
    public int FormatId { get; set; }
    public bool Reprint { get; set; }
    public DateTime? DateAdded { get; set; }
    public string? Summary { get; set; }
    public DateTime? DateLastUpdated { get; set; }
    public DateTime? CoverDate { get; set; }
}
```

---

# 🔌 Repository Interfaces

## Volume Repository
```
public interface IVolumeRepository
{
    Task AddVolumeAsync(Volume volume);
    Task<List<int>> GetAllVolumeId();
}
```

## Issue Repository
```
public interface IIssueRepository
{
    Task AddIssueAsync(Issue issue);
    Task<List<int>> GetAllIssueIds();
}
```

---

# 🌐 ComicVine Integration

## WebDataService must expose:
```
IEnumerable<Volume> GetVolumesFromPublisher(int publisherId);
IEnumerable<Issue> GetIssuesFromVolume(int volumeId);
```

Returned models must match the Domain Models exactly.

---

# 🧠 Use Case Rules

## Naming
- Volume use cases live in `Application.UseCases.Volumes`
- Issue use cases live in `Application.UseCases.Issues`
- ComicVine use cases live in `Application.UseCases.ComicVine`

## Examples

### AddVolumeUseCase
```
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
```
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

### SearchMissingComicVineVolumesByPublisherUseCase
```
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

### SearchMissingComicVineIssuesByVolumeUseCase
```
public class SearchMissingComicVineIssuesByVolumeUseCase : ISearchMissingComicVineIssuesByVolumeUseCase
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

## AddVolume Page
```
@page "/AddVolume/{pubId:int}"
<ComicVineVolumeListComponent PublisherId="pubId" />
```

## AddIssue Page
```
@page "/AddIssue/{volumeId:int}"
<ComicVineIssueListComponent VolumeId="volumeId" />
```

## ComicVineVolumeListComponent  
- Injects `ISearchMissingComicVineVolumesByPublisherUseCase`
- Displays table of missing volumes
- Renders `<ComicVineVolumeListItemComponent>`

## ComicVineIssueListComponent  
- Injects `ISearchMissingComicVineIssuesByVolumeUseCase`
- Displays table of missing issues
- Renders `<ComicVineIssueListItemComponent>`

## ComicVineVolumeListItemComponent  
- Injects `IAddVolumeUseCase`
- Button: “Add Volume”
- Navigates to `/Volumes/{PublisherId}`

## ComicVineIssueListItemComponent  
- Injects `IAddIssueUseCase`
- Button: “Add Issue”
- Navigates to `/Issues/{VolumeId}`

---

# 🎨 CSS Utilities

## Wrap long text in description columns
```
.wrap-long-text {
    white-space: normal;
    word-break: break-word;
}
```

## Narrow button columns
```
.narrow-col {
    width: 1%;
    white-space: nowrap;
}
```

---

# 🔗 ComicVine Links

## Publisher link
```
<a href="https://comicvine.gamespot.com/publisher/4010-@(Publisher.Id)/"
   target="_blank"
   rel="noopener noreferrer">
```

## Volume link
```
https://comicvine.gamespot.com/volume/4050-{Volume.Id}/
```

## Issue link
```
https://comicvine.gamespot.com/issue/4000-{Issue.Id}/
```

---

# 🧭 Navigation Rules

- After adding a Volume → navigate to `/Volumes/{PublisherId}`
- After adding an Issue → navigate to `/Issues/{VolumeId}`
- Add pages always follow pattern:  
  `/AddVolume/{pubId}`  
  `/AddIssue/{volumeId}`

---

# 🛠️ Development Rules

- All new code must follow the patterns defined above  
- All Issue functionality must mirror Volume functionality 1:1  
- No repository or EF Core logic in UI or Use Cases  
- No UI logic in Use Cases  
- No ComicVine logic in UI or Repositories  
- All new components must follow existing naming conventions  
- All new use cases must follow existing async patterns  
- All new pages must follow existing routing patterns  

---

# ✔️ End of Prompt  
Paste this entire block into `prompt.md` to bootstrap any new session.
