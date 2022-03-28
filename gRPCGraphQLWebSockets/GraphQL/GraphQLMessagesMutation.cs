using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.GraphQL.Model;
using gRPCGraphQLWebSockets.SharedModel;

namespace gRPCGraphQLWebSockets.GraphQL
{
    public class GraphQLMessagesMutation
    {
        public GraphQLMessageCreatedPayload CreateMessage(GraphQLNewMessage newMessage)
        {
            var message = new Message(newMessage);

            using (var context = new gRPCGraphQLWebSocketsDatabaseContext())
            {
                context.Add(message);

                context.SaveChanges();
            }

            return new GraphQLMessageCreatedPayload {Id = message.Id};
        }
    }
}