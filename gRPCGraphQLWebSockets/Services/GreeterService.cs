using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using gRPCGraphQLWebSockets.Services.Proto;

namespace gRPCGraphQLWebSockets.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        public GreeterService()
        {
            
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}