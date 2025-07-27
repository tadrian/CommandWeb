# CommandWeb

A Blazor Server application for managing and executing custom commands. This project demonstrates real-time command execution, data display, and integration with Radzen Blazor components.

## Features

- Add, edit, and run custom commands
- Real-time updates using SignalR
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
