using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.gRPC.Proto;
using gRPCGraphQLWebSockets.SharedModel;

namespace gRPCGraphQLWebSockets.gRPC
{
    public class gRPCMessagesService : Proto.gRPCMessagesService.gRPCMessagesServiceBase
    {
        public gRPCGraphQLWebSocketsDatabaseContext _context;

        public gRPCMessagesService(gRPCGraphQLWebSocketsDatabaseContext context)
        {
            _context = context;
        }


        public override Task<gRPCGetMessagesResponse> GetMessages(gRPCGetMessagesRequest messageRequest,
            ServerCallContext context)
        {
            var messages = _context.Messages.ToList();

            var getMessagesResponse = new gRPCGetMessagesResponse();

            foreach (var message in messages)
                getMessagesResponse.Messages.Add(new gRPCMessage
                {
                    Id = message.Id,
                    Text = message.Text
                });

            return Task.FromResult(getMessagesResponse);
        }

        public override Task<gRPCCreateMessageResponse> CreateMessage(gRPCCreateMessageRequest messageRequest,
            ServerCallContext context)
        {
            var message = new Message(messageRequest);

            _context.Add(message);

            _context.SaveChanges();

            return Task.FromResult(new gRPCCreateMessageResponse
            {
                Id = message.Id
            });
        }
    }
}