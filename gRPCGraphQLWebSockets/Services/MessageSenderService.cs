using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.Model;
using Microsoft.Extensions.Logging;
using gRPCGraphQLWebSockets.Services.Proto;

namespace gRPCGraphQLWebSockets.Services
{
    public class MessageSenderService : MessageSender.MessageSenderBase 
    {
        public gRPCGraphQLWebSocketsDatabaseContext _context;
        
        public MessageSenderService(gRPCGraphQLWebSocketsDatabaseContext context)
        {
            _context = context;
        }

        public override Task<ReceiveMessageReply> ReceiveMessage(ReceiveMessageRequest messageRequest, ServerCallContext context)
        {
             var message = _context.Messages.FirstOrDefault(m => m.Id == messageRequest.Id);
            
             return Task.FromResult(new ReceiveMessageReply()
             {
                 Id = message.Id,
                 Text = message.Text
             });
        } 
        
        public override Task<SendMessageReply> SendMessage(SendMessageRequest messageRequest, ServerCallContext context)
        {
            var message = new Message()
            {
                Text = messageRequest.MessagePayload.Text
            };
              
            _context.Add(message);
            
            _context.SaveChanges();
       
            return Task.FromResult(new SendMessageReply()
            {
                Id = message.Id
            });
        }
    }
}