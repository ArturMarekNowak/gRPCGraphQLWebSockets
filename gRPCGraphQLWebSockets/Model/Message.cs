#nullable disable

namespace gRPCGraphQLWebSockets.Model
{
    public sealed class Message
    {
        public Message()
        {
            
        }
        
        public Message(string messageText)
        {
            Text = messageText;
        }
        
        public long Id { get; set; }
        public string Text { get; set; }
    }
}
