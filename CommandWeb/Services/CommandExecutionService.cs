using System.Diagnostics;
using System.Text;
using System.Threading.Channels;

namespace CommandWeb.Services
{
    /// <summary>
    /// Service for securely executing CLI and PowerShell commands and streaming output.
    /// </summary>
    public interface ICommandExecutionService
    {
        /// <summary>
        /// Executes a command and streams output lines asynchronously.
        /// </summary>
        /// <param name="command">The command to execute (e.g., "pwsh" or "cmd").</param>
        /// <param name="arguments">Arguments for the command.</param>
        /// <returns>A channel reader for streaming output lines.</returns>
        ChannelReader<string> ExecuteCommandStream(string command, string arguments, string? workingDirectory = null);
    }

    /// <summary>
    /// Implementation of ICommandExecutionService using System.Diagnostics.Process.
    /// </summary>
    public class CommandExecutionService : ICommandExecutionService
    {
        public ChannelReader<string> ExecuteCommandStream(string command, string arguments, string? workingDirectory = null)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentException("Command cannot be null or empty.", nameof(command));

            var channel = Channel.CreateUnbounded<string>();

            _ = Task.Run(async () =>
            {
                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = command,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    if (!string.IsNullOrWhiteSpace(workingDirectory))
                    {
                        psi.WorkingDirectory = workingDirectory;
                    }

                    using var process = new Process { StartInfo = psi, EnableRaisingEvents = true };

                    process.OutputDataReceived += (s, e) =>
                    {
                        if (e.Data != null)
                            channel.Writer.TryWrite(e.Data);
                    };
                    process.ErrorDataReceived += (s, e) =>
                    {
                        if (e.Data != null)
                            channel.Writer.TryWrite($"ERROR: {e.Data}");
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    await process.WaitForExitAsync();
                }
                catch (Exception ex)
                {
                    channel.Writer.TryWrite($"Exception: {ex.Message}");
                }
                finally
                {
                    channel.Writer.Complete();
                }
            });

            return channel.Reader;
        }
    }
}
