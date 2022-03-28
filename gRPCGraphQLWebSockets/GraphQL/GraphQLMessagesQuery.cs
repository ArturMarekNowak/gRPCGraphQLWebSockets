using System.Collections.Generic;
using System.Linq;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.SharedModel;

namespace gRPCGraphQLWebSockets.GraphQL
{
    public class GraphQLMessagesQuery
    {
        public List<Message> GetMessages()
        {
            var messages = new List<Message>();

            using (var context = new gRPCGraphQLWebSocketsDatabaseContext())
            {
                messages = context.Messages.ToList();
            }

            return messages;
        }
    }
}