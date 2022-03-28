using System.Collections.Generic;
using gRPCGraphQLWebSockets.Rest.Model;
using gRPCGraphQLWebSockets.SharedModel;

namespace gRPCGraphQLWebSockets.Rest.Services.Interfaces
{
    public interface IRESTMessagesService
    {
        public List<Message> GetMessages();

        public long CreateMessage(RESTNewMessage newMessage);
    }
}