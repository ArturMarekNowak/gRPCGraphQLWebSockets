using System.Collections.Generic;
using System.Threading.Tasks;
using gRPCGraphQLWebSockets.Rest.Model;
using gRPCGraphQLWebSockets.SharedModel;

namespace gRPCGraphQLWebSockets.Rest.Services.Interfaces
{
    public interface IRESTMessagesService
    {
        public List<Message> GetMessages();

        public Task<long> CreateMessage(RESTNewMessage newMessage);
    }
}