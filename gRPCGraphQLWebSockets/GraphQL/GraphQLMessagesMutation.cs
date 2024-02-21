using System.Threading.Tasks;
using GrpcGraphQlWebSockets.Database;
using GrpcGraphQlWebSockets.GraphQL.Model;
using GrpcGraphQlWebSockets.SharedModel;
using HotChocolate;

namespace GrpcGraphQlWebSockets.GraphQL
{
    public class GraphQlMessagesMutation
    {
        public async Task<GraphQlMessageCreatedPayload> CreateMessage(
            [Service] GrpcGraphQlWebSocketsDatabaseContext context, GraphQlNewMessage newMessage)
        {
            var message = new Message(newMessage);

            await context.AddAsync(message);

            await context.SaveChangesAsync();

            return new GraphQlMessageCreatedPayload {Id = message.Id};
        }
    }
}