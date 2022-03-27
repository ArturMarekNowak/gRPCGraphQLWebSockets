using System.Collections.Generic;
using gRPCGraphQLWebSockets.Model;
using gRPCGraphQLWebSockets.Rest.Model;

namespace gRPCGraphQLWebSockets.Rest.Services.Interfaces
{
    public interface IRESTMessagesService
    {
        public List<Message> GetMessages();

        public long CreateMessage(RESTNewMessage newMessage);
    }
}