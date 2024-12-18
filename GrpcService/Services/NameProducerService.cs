using Grpc.Core;
using gRPC_GuidServer;
using gRPC_NameProducerServer;

namespace gRPC_Server.Services
{
    public class NameProducerService: NameProducer.NameProducerBase
    {
        public override async Task CreateName(IAsyncStreamReader<Request_Name> requestStream, IServerStreamWriter<Response_Name> responseStream, ServerCallContext context)
        {
            
            while(await requestStream.MoveNext(context.CancellationToken))
            {
                string incomingName = requestStream.Current.Name;
                Console.WriteLine($"Incoming name: {incomingName}");
                string updatedName = UpdateName(incomingName);
                await responseStream.WriteAsync(new Response_Name
                {
                    Name = updatedName
                });
            }

        }
        private string UpdateName(string name)
        {
            return $"{name}_Updated_{new Random().Next(100)}";
        }
    }
}