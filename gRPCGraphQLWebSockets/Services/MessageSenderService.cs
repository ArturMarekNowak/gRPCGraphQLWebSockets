using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.Model;
using gRPCGraphQLWebSockets.Services.Proto;
using Message = gRPCGraphQLWebSockets.Model.Message;

namespace gRPCGraphQLWebSockets.Services
{
    public class MessageSenderService 
    {
        public gRPCGraphQLWebSocketsDatabaseContext _context;
        
        public MessageSenderService(gRPCGraphQLWebSocketsDatabaseContext context)
        {
            _context = context;
        }

        public Task<GetMessagesResponse> GetMessages(GetMessagesRequest messageRequest, ServerCallContext context)
        {
             var messages = _context.Messages.ToList();
             
             var getMessagesResponse = new GetMessagesResponse();
             getMessagesResponse.Messages.AddRange(messages.Cast<gRPCGraphQLWebSockets.Services.Proto.Message>());
             
             return Task.FromResult(getMessagesResponse);
        } 
        
        public Task<CreateMessageResponse> CreateMessage(CreateMessageRequest messageRequest, ServerCallContext context)
        {
            var message = new Model.Message()
            {
                Text = messageRequest.MessagePayload.Text
            };
              
            _context.Add(message);
            
            _context.SaveChanges();
       
            return Task.FromResult(new CreateMessageResponse()
            {
                Id = message.Id
            });
        }
    }
}