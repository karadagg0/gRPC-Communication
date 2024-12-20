using Grpc.Core;
using gRPC_Server;

namespace gRPC_Server.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Name cannot be empty."));
            }

            await Task.Delay(100);

            return new HelloReply
            {
                Message = $"Hello, {request.Name.Trim()}! Welcome to our service."
            };
        }
    }
}