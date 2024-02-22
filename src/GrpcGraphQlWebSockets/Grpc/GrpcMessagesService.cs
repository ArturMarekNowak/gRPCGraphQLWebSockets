using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcGraphQlWebSockets.Database;
using GrpcGraphQLWebSockets.Grpc.Proto;
using GrpcGraphQlWebSockets.SharedModel;

namespace GrpcGraphQlWebSockets.Grpc
{
    public class GrpcMessagesService : GrpcGraphQLWebSockets.Grpc.Proto.GrpcMessagesService.GrpcMessagesServiceBase
    {
        public GrpcGraphQlWebSocketsDatabaseContext _context;

        public GrpcMessagesService(GrpcGraphQlWebSocketsDatabaseContext context)
        {
            _context = context;
        }


        public override Task<GrpcGetMessagesResponse> GetMessages(GrpcGetMessagesRequest messageRequest,
            ServerCallContext context)
        {
            var messages = _context.Messages.ToList();

            var getMessagesResponse = new GrpcGetMessagesResponse();

            foreach (var message in messages)
                getMessagesResponse.Messages.Add(new GrpcMessage
                {
                    Id = message.Id,
                    Text = message.Text
                });

            return Task.FromResult(getMessagesResponse);
        }

        public override async Task<GrpcCreateMessageResponse> CreateMessage(GrpcCreateMessageRequest messageRequest,
            ServerCallContext context)
        {
            var message = new Message(messageRequest);

            await _context.AddAsync(message);

            await _context.SaveChangesAsync();

            return await Task.FromResult(new GrpcCreateMessageResponse
            {
                Id = message.Id
            });
        }
    }
}