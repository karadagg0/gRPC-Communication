using Grpc.Core;
using gRPC_GuidServer;

namespace gRPC_Server.Services
{
    public class GuidStreamService:GuidProducer.GuidProducerBase
    {
        public override async Task SendGuid(GuidRequest request, IServerStreamWriter<GuidResponse> responseStream, ServerCallContext context)
        {
            for(int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new GuidResponse
                {
                    Guid = $"{i+1} => {Guid.NewGuid()}"
                });
            }
        }

    }
}