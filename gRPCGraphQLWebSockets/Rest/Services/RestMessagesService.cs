using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcGraphQlWebSockets.Database;
using GrpcGraphQlWebSockets.Rest.Model;
using GrpcGraphQlWebSockets.Rest.Services.Interfaces;
using GrpcGraphQlWebSockets.SharedModel;

namespace GrpcGraphQlWebSockets.Rest.Services
{
    public class RestMessagesService : IRestMessagesService
    {
        private readonly GrpcGraphQlWebSocketsDatabaseContext _context;

        public RestMessagesService(GrpcGraphQlWebSocketsDatabaseContext context)
        {
            _context = context;
        }

        public List<Message> GetMessages()
        {
            var messages = _context.Messages.ToList();

            return messages;
        }

        public async Task<long> CreateMessage(RestNewMessage newMessage)
        {
            var message = new Message(newMessage);

            await _context.AddAsync(message);

            await _context.SaveChangesAsync();

            return message.Id;
        }
    }
}