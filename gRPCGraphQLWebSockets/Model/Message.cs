#nullable disable

namespace gRPCGraphQLWebSockets.Model
{
    public sealed class Message
    {
        public Message()
        {
            
        }
        
        public Message(NewMessage newMessage)
        {
            Text = newMessage.Text;
        }
        
        public long Id { get; set; }
        public string Text { get; set; }
    }
}
