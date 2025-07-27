using Microsoft.AspNetCore.SignalR;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CommandWeb.Hubs
{
    /// <summary>
    /// SignalR hub for streaming command output to clients.
    /// </summary>
    public class CommandHub : Hub
    {
        // This method will be called by the backend to stream output to clients
        public async Task StreamCommandOutput(string commandId, ChannelReader<string> output)
        {
            await foreach (var line in output.ReadAllAsync())
            {
                await Clients.Caller.SendAsync("ReceiveCommandOutput", commandId, line);
            }
        }
    }
}
