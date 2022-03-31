using System.Threading.Tasks;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.GraphQL.Model;
using gRPCGraphQLWebSockets.SharedModel;
using HotChocolate;

namespace gRPCGraphQLWebSockets.GraphQL
{
    public class GraphQLMessagesMutation
    {
        public async Task<GraphQLMessageCreatedPayload> CreateMessage(
            [Service] gRPCGraphQLWebSocketsDatabaseContext context, GraphQLNewMessage newMessage)
        {
            var message = new Message(newMessage);

            await context.AddAsync(message);

            await context.SaveChangesAsync();

            return new GraphQLMessageCreatedPayload {Id = message.Id};
        }
    }
}