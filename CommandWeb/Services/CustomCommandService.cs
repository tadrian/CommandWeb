

using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommandWeb.Services;
using System.Reflection;

namespace CommandWeb.Services;
    // CustomCommand class moved to its own file: CustomCommand.cs

    /// <summary>
    /// Service for CRUD operations on custom commands.
    /// </summary>
    public interface ICustomCommandService
    {
        IEnumerable<CustomCommand> GetAll();
        CustomCommand? Get(Guid id);
        CustomCommand Add(CustomCommand command);
        bool Update(CustomCommand command);
        bool Delete(Guid id);
    }

    /// <summary>
    /// In-memory implementation of ICustomCommandService.
    /// </summary>
    public class CustomCommandService : ICustomCommandService
    {
        private readonly ConcurrentDictionary<Guid, CustomCommand> _commands = new();
        private readonly string _jsonPath;
        private readonly object _fileLock = new();

        public CustomCommandService()
        {
            // Determine the path to Data/commands.json relative to the assembly location
            var baseDir = AppContext.BaseDirectory;
            var dataDir = Path.Combine(baseDir, "Data");
            _jsonPath = Path.Combine(dataDir, "commands.json");
            if (!File.Exists(_jsonPath))
            {
                Directory.CreateDirectory(dataDir);
                File.WriteAllText(_jsonPath, "[]");
            }
            LoadFromFile();
        }

        public IEnumerable<CustomCommand> GetAll() => _commands.Values.OrderByDescending(c => c.CreatedAt);

        public CustomCommand? Get(Guid id) => _commands.TryGetValue(id, out var cmd) ? cmd : null;

        public CustomCommand Add(CustomCommand command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;
            _commands[command.Id] = command;
            SaveToFile();
            return command;
        }

        public bool Update(CustomCommand command)
        {
            if (!_commands.ContainsKey(command.Id)) return false;
            _commands[command.Id] = command;
            SaveToFile();
            return true;
        }

        public bool Delete(Guid id)
        {
            var result = _commands.TryRemove(id, out _);
            if (result) SaveToFile();
            return result;
        }

        private void LoadFromFile()
        {
            lock (_fileLock)
            {
                try
                {
                    var json = File.ReadAllText(_jsonPath);
                    var items = JsonSerializer.Deserialize<List<CustomCommand>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    _commands.Clear();
                    if (items != null)
                    {
                        foreach (var cmd in items)
                        {
                            if (cmd.Id == Guid.Empty) cmd.Id = Guid.NewGuid();
                            if (cmd.CreatedAt == default) cmd.CreatedAt = DateTime.UtcNow;
                            _commands[cmd.Id] = cmd;
                        }
                    }
                }
                catch { /* Ignore errors, start empty */ }
            }
        }

        private void SaveToFile()
        {
            lock (_fileLock)
            {
                try
                {
                    var list = _commands.Values.OrderByDescending(c => c.CreatedAt).ToList();
                    var json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(_jsonPath, json);
                }
                catch { /* Ignore errors */ }
            }
        }
    }
