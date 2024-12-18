
using Grpc.Core;
using gRPC_SumServer;

namespace gRPC_Server.Services
{
    public class SumService:Sum.SumBase
    {
        public override async  Task<ResponseValue> SumValues(IAsyncStreamReader<Values> requestStream, ServerCallContext context)
        {

            uint count = 0;
           while(await requestStream.MoveNext(context.CancellationToken))
            {
                Console.WriteLine(requestStream.Current.Value);
                count += requestStream.Current.Value;
            }
             return new ResponseValue() { SumOfValues = $"The sum of the numbers is {count}" };
        }
    }
}