using gRPCGraphQLWebSockets.Model;

namespace gRPCGraphQLWebSockets.Services.Intefaces
{
    public interface IMessagesService
    {
        public Message GetMessage(long messageId);

        public long AddMessage(NewMessage newMessage);
    }
}