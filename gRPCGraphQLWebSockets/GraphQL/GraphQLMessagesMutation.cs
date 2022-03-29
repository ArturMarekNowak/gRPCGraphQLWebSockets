using System.Threading.Tasks;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.GraphQL.Model;
using gRPCGraphQLWebSockets.SharedModel;

namespace gRPCGraphQLWebSockets.GraphQL
{
    public class GraphQLMessagesMutation
    {
        private readonly gRPCGraphQLWebSocketsDatabaseContext _context;

        public GraphQLMessagesMutation(gRPCGraphQLWebSocketsDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GraphQLMessageCreatedPayload> CreateMessage(GraphQLNewMessage newMessage)
        {
            var message = new Message(newMessage);

            await _context.AddAsync(message);

            await _context.SaveChangesAsync();

            return new GraphQLMessageCreatedPayload {Id = message.Id};
        }
    }
}