using System.Threading.Tasks;
using GrpcGraphQlWebSockets.Database;
using GrpcGraphQlWebSockets.SharedModel;
using Microsoft.AspNetCore.SignalR;

namespace GrpcGraphQlWebSockets.SignalR
{
    public class SignalRMessageHub : Hub
    {
        private readonly GrpcGraphQlWebSocketsDatabaseContext _context;

        public SignalRMessageHub(GrpcGraphQlWebSocketsDatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateMessage(string text)
        {
            var message = new Message {Text = text};

            await _context.Messages.AddAsync(message);

            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", "id: " + message.Id);
        }
    }
}