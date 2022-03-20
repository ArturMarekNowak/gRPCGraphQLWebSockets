using System.Linq;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.Model;
using gRPCGraphQLWebSockets.Services.Intefaces;

namespace gRPCGraphQLWebSockets.Services
{
    public class MessagesService : IMessagesService
    {
        public gRPCGraphQLWebSocketsDatabaseContext _context;

        public MessagesService(gRPCGraphQLWebSocketsDatabaseContext context)
        {
            _context = context;
        }
        
        public Message GetMessage(int messageId)
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == messageId);

            return message;
        }

        public long AddMessage(NewMessage newMessage)
        {
            var message = new Message(newMessage);

            _context.Add(message);

            _context.SaveChanges();

            return message.Id;
        }
    }
}