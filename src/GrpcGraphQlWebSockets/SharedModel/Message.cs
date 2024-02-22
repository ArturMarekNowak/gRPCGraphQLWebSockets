using System;
using GrpcGraphQlWebSockets.GraphQL.Model;
using GrpcGraphQlWebSockets.Rest.Model;
using GrpcGraphQLWebSockets.Grpc.Proto;

#nullable disable

namespace GrpcGraphQlWebSockets.SharedModel
{
    public sealed class Message
    {
        public Message()
        {
        }

        public Message(RestNewMessage newMessage)
        {
            Text = newMessage.Text;
        }

        public Message(GrpcCreateMessageRequest grpcCreateMessageRequest)
        {
            Text = grpcCreateMessageRequest.MessagePayload.Text;
        }

        public Message(GraphQlNewMessage grpcCreateMessageRequest)
        {
            Text = grpcCreateMessageRequest.Text;
        }

        public long Id { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{DateTime.Now}: {Text}";
        }
    }
}