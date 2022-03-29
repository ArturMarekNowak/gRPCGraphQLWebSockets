using System;
using gRPCGraphQLWebSockets.GraphQL.Model;
using gRPCGraphQLWebSockets.gRPC.Proto;
using gRPCGraphQLWebSockets.Rest.Model;

#nullable disable

namespace gRPCGraphQLWebSockets.SharedModel
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

        public Message(GraphQLNewMessage gRpcCreateMessageRequest)
        {
            Text = gRpcCreateMessageRequest.Text;
        }

        public long Id { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{DateTime.Now}: {Text}";
        }
    }
}