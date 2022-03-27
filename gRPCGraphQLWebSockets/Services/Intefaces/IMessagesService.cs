using System.Collections.Generic;
using gRPCGraphQLWebSockets.Model;

namespace gRPCGraphQLWebSockets.Services.Intefaces
{
    public interface IMessagesService
    {
        public List<Message> GetMessages();

        public long CreateMessage(NewMessage newMessage);
    }
}