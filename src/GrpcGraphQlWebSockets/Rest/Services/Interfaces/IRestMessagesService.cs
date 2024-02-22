using System.Collections.Generic;
using System.Threading.Tasks;
using GrpcGraphQlWebSockets.Rest.Model;
using GrpcGraphQlWebSockets.SharedModel;

namespace GrpcGraphQlWebSockets.Rest.Services.Interfaces
{
    public interface IRestMessagesService
    {
        public List<Message> GetMessages();

        public Task<long> CreateMessage(RestNewMessage newMessage);
    }
}