using gRPCGraphQLWebSockets.gRPC.Proto;
using gRPCGraphQLWebSockets.Rest.Model;

#nullable disable

namespace gRPCGraphQLWebSockets.Model
{
    public sealed class Message
    {
        public Message()
        {
            
        }
        
        public Message(RESTNewMessage newMessage)
        {
            Text = newMessage.Text;
        }

        public Message(gRPCCreateMessageRequest gRpcCreateMessageRequest)
        {
            Text = gRpcCreateMessageRequest.MessagePayload.Text;
        }
        
        public long Id { get; set; }
        public string Text { get; set; }
    }
}
