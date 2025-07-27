# CommandWeb

A modern, minimal, and extensible Blazor Server app for running, managing, and categorizing custom shell/CLI commands with real-time streaming output. Built with Radzen components and persistent JSON storage.

---

## Features

- **Run PowerShell, CMD, .NET CLI, npm, and custom commands**
- **Categorized left-pane command list** with collapsible/expandable groups
- **Search/filter** commands instantly
- **Add, edit, and delete commands** via dialog
- **Persistent storage** in `Data/commands.json`
- **Working directory** support per command
- **Live streaming output** for long-running commands
- **Minimal, professional UI** (Radzen, Bootstrap, custom font)

---

## Prerequisites

- [.NET 8.0+ SDK](https://dotnet.microsoft.com/download)
- Windows (PowerShell, CMD support)
- Node.js (for npm commands, optional)

---

## Getting Started

1. **Clone or unzip the project**
2. *(Optional but recommended)* Move the project to a simple path (e.g., `C:\App19`)
3. **Restore dependencies:**
   ```sh
   dotnet restore
   ```
4. **Build the project:**
   ```sh
   dotnet build
   ```
5. **Run the app:**
   ```sh
   dotnet run --project CommandWeb/CommandWeb.csproj
   ```
6. **Open your browser:**
   - Navigate to `https://localhost:5001` or the URL shown in the console

---

## Usage

- **Browse and filter commands** in the left pane
- **Click a command** to view details and output
- **Run**: Click the play button to execute and stream output
- **Edit**: Click the edit button to modify a command
- **Add**: Use the "Add Command" button to create new commands
- **Delete**: Use the delete button in the edit dialog
- **Categories**: Collapse/expand groups for organization

---

## Command Persistence

- All commands are stored in `Data/commands.json` (auto-created)
- Supports PowerShell, CMD, .NET CLI, npm, and any custom shell command
- Each command has:
  - Name
  - Command (e.g., `pwsh`, `cmd`, `dotnet`, `npm`)
  - Arguments
  - Category
  - Working Directory (optional)

---

## Project Structure

```
CommandWeb/
  Pages/           # Blazor pages (Commands.razor, AddCommandDialog.razor, etc.)
  Services/        # Command execution, persistence, and models
  Data/            # Persistent commands.json storage
  Shared/          # Layout and shared UI
  wwwroot/         # Static assets (CSS, JS)
  _Imports.razor   # Global usings
  Program.cs       # App startup and DI
  README.md        # This file
```

---

## Troubleshooting

- **Radzen or service types not recognized in Razor pages?**
  - Ensure `_Imports.razor` includes:
    ```
    @using CommandWeb.Services
    @using Radzen
    @using Radzen.Blazor
    ```
  - Set `<RootNamespace>CommandWeb</RootNamespace>` in `CommandWeb.csproj`
  - Move the project to a simple path (e.g., `C:\App19`)
  - Run `dotnet clean` and `dotnet build`

- **No output or errors running commands?**
  - Check that the command and arguments are valid for your OS
  - For npm commands, ensure Node.js is installed and in your PATH

- **Persistent storage issues?**
  - Ensure the app can write to `Data/commands.json`

---

## Customization

- Add more categories or commands via the UI or by editing `commands.json`
- Extend `CustomCommandService` for advanced storage (e.g., database)
- Style the UI by editing `wwwroot/css/site.css` or layout files

---

## Contributing

Pull requests and suggestions are welcome! Please open an issue for bugs or feature requests.

---

## License

MIT License. See `LICENSE.txt` for details.
