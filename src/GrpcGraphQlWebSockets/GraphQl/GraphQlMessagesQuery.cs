using System.Collections.Generic;
using System.Linq;
using GrpcGraphQlWebSockets.Database;
using GrpcGraphQlWebSockets.SharedModel;
using HotChocolate;

namespace GrpcGraphQlWebSockets.GraphQL
{
    public class GraphQlMessagesQuery
    {
        public List<Message> GetMessages([Service] GrpcGraphQlWebSocketsDatabaseContext context)
        {
            var messages = new List<Message>();

            messages = context.Messages.ToList();

            return messages;
        }
    }
}