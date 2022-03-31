using System.Collections.Generic;
using System.Linq;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.SharedModel;
using HotChocolate;

namespace gRPCGraphQLWebSockets.GraphQL
{
    public class GraphQLMessagesQuery
    {
        public List<Message> GetMessages([Service] gRPCGraphQLWebSocketsDatabaseContext context)
        {
            var messages = new List<Message>();

            messages = context.Messages.ToList();

            return messages;
        }
    }
}