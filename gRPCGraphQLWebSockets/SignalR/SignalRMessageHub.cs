using System.Threading.Tasks;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.SharedModel;
using Microsoft.AspNetCore.SignalR;

namespace gRPCGraphQLWebSockets.SignalR
{
    public class SignalRMessageHub : Hub
    {
        private readonly gRPCGraphQLWebSocketsDatabaseContext _context;

        public SignalRMessageHub(gRPCGraphQLWebSocketsDatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateMessage(string text)
        {
            var message = new Message {Text = text};

            await _context.Messages.AddAsync(message);

            await _context.SaveChangesAsync();
        }
    }
}