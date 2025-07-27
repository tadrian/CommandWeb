
# CommandWeb

CommandWeb is a modern Blazor Server application that lets you create, manage, and execute custom shell commands directly from your browser. It’s designed for power users, sysadmins, and developers who want a real-time, web-based command center for their scripts and automation tasks.

## Why CommandWeb?

- **Centralized Command Management:** Organize all your custom scripts and commands in one place, accessible from anywhere on your local network.
- **Real-Time Console Streaming:** See command output as it happens, just like a terminal, but in your browser.
- **Modern UI:** Built with Radzen Blazor components for a responsive, interactive experience.
- **Extensible:** Easily add new commands, automate tasks, and monitor results live.

### What Makes It Cool?

- **Live Console Output:** No more waiting for a command to finish—watch the output stream in real time.
- **Web-Based Control:** Run and monitor commands from any device with a browser.
- **Safe by Design:** By default, the app only binds to localhost for security.

## How SignalR Powers Real-Time Streaming

CommandWeb uses [SignalR](https://learn.microsoft.com/aspnet/core/signalr/introduction) to enable real-time, bi-directional communication between the server and your browser. When you run a command, the server executes it and streams the console output line-by-line to all connected clients instantly. This means you see the command’s progress and results live, just like in a native terminal, but with the convenience of a web UI.

**How it works:**

1. You trigger a command from the web interface.
2. The server starts the process and captures its output.
3. As each line is produced, SignalR pushes it to your browser in real time.
4. The UI updates instantly, showing you the live console output.

This makes CommandWeb ideal for monitoring long-running scripts, automation, or any process where immediate feedback is valuable.

## Features

- Add, edit, and run custom commands
- Real-time console output streaming with SignalR
- Responsive UI with Radzen Blazor components
- Weather forecast sample page

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (if you plan to use frontend tooling)

### Setup

1. Clone the repository:
   ```sh
   git clone <your-github-repo-url>
   cd CommandWeb
   ```
2. Restore dependencies:
   ```sh
   dotnet restore
   ```
3. Build and run the application:
   ```sh
   dotnet run --project CommandWeb/CommandWeb.csproj
   ```

4. Open your browser and navigate to `https://localhost:5001` (or the URL shown in the terminal).

## Project Structure

- `CommandWeb/Pages/` - Blazor pages
- `CommandWeb/Services/` - Backend services
- `CommandWeb/Hubs/` - SignalR hubs
- `CommandWeb/Data/` - Data models and services
- `CommandWeb/Shared/` - Shared UI components

## Usage

- Use the navigation menu to access different features.
- Add or run commands from the Commands page.
- View weather data on the FetchData page.

## Contributing

Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License.
