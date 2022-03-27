using System.Collections.Generic;
using System.Linq;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.Model;
using gRPCGraphQLWebSockets.Services.Intefaces;

namespace gRPCGraphQLWebSockets.Services
{
    public class MessagesService : IMessagesService
    {
        private gRPCGraphQLWebSocketsDatabaseContext _context;

        public MessagesService(gRPCGraphQLWebSocketsDatabaseContext context)
        {
            _context = context;
        }
        
        public List<Message> GetMessages()
        {
            var messages = _context.Messages.ToList();

            return messages;
        }

        public long CreateMessage(NewMessage newMessage)
        {
            var message = new Message(newMessage);

            _context.Add(message);

            _context.SaveChanges();

            return message.Id;
        }
    }
}