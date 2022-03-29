using System.Collections.Generic;
using System.Linq;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.SharedModel;

namespace gRPCGraphQLWebSockets.GraphQL
{
    public class GraphQLMessagesQuery
    {
        private readonly gRPCGraphQLWebSocketsDatabaseContext _context;

        public GraphQLMessagesQuery(gRPCGraphQLWebSocketsDatabaseContext context)
        {
            _context = context;
        }

        public List<Message> GetMessages()
        {
            var messages = new List<Message>();

            messages = _context.Messages.ToList();

            return messages;
        }
    }
}