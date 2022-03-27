using System.Collections.Generic;
using System.Linq;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.Model;
using gRPCGraphQLWebSockets.Rest.Model;
using gRPCGraphQLWebSockets.Rest.Services.Interfaces;

namespace gRPCGraphQLWebSockets.Rest.Services
{
    public class RESTMessagesService : IRESTMessagesService
    {
        private gRPCGraphQLWebSocketsDatabaseContext _context;

        public RESTMessagesService(gRPCGraphQLWebSocketsDatabaseContext context)
        {
            _context = context;
        }
        
        public List<Message> GetMessages()
        {
            var messages = _context.Messages.ToList();

            return messages;
        }

        public long CreateMessage(RESTNewMessage newMessage)
        {
            var message = new Message(newMessage);

            _context.Add(message);

            _context.SaveChanges();

            return message.Id;
        }
    }
}