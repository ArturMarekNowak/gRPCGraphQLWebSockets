using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.Rest.Model;
using gRPCGraphQLWebSockets.Rest.Services.Interfaces;
using gRPCGraphQLWebSockets.SharedModel;

namespace gRPCGraphQLWebSockets.Rest.Services
{
    public class RESTMessagesService : IRESTMessagesService
    {
        private readonly gRPCGraphQLWebSocketsDatabaseContext _context;

        public RESTMessagesService(gRPCGraphQLWebSocketsDatabaseContext context)
        {
            _context = context;
        }

        public List<Message> GetMessages()
        {
            var messages = _context.Messages.ToList();

            return messages;
        }

        public async Task<long> CreateMessage(RESTNewMessage newMessage)
        {
            var message = new Message(newMessage);

            await _context.AddAsync(message);

            await _context.SaveChangesAsync();

            return message.Id;
        }
    }
}