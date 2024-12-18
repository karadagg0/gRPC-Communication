using Grpc.Core;
using Grpc.Net.Client;

namespace gRPC_Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7016");
            var greetClient = new Greeter.GreeterClient(channel);
            HelloReply helloReply = await greetClient.SayHelloAsync(new HelloRequest
            {
                Name = "Test_Name"
            });
            Console.WriteLine($"{helloReply.Message}");
            var messageClient = new Message.MessageClient(channel);

            MessageResponse messageResponse = await messageClient.SendMessageAsync(new MessageRequest
            {
                Message = helloReply.Message,
                Name = "Ekin"
            });
            Console.WriteLine($"{messageResponse.Message}");

            Task.Delay(1000).Wait();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Server Stream");
            /*     Server  Stream     */
            var guidClient = new GuidProducer.GuidProducerClient(channel);

            var guidResponse = guidClient.SendGuid(new GuidRequest { Guid = "" });
            CancellationTokenSource cts = new CancellationTokenSource();

            while (await guidResponse.ResponseStream.MoveNext(cts.Token))
            {
                Console.WriteLine(guidResponse.ResponseStream.Current.Guid);
            }

            /*    Client Stream     */
            Task.Delay(1000).Wait();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Client Stream");
            var sumClient = new Sum.SumClient(channel);
            var request = sumClient.SumValues();
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(100);
                await request.RequestStream.WriteAsync(new Values
                {
                    Value = (uint)new Random().Next(100)
                });
                
            }
            await request.RequestStream.CompleteAsync();
            string response = (await request.ResponseAsync).SumOfValues;
            Console.WriteLine($"{response}");

            /*      Both Client And Server Streaming    */
            Task.Delay (1000).Wait();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Both Client And Server Streaming");
            var nameClient = new NameProducer.NameProducerClient(channel);
            using var updateNameRequest = nameClient.CreateName();
            string[] names = { "James", "Olivia", "Michael", "Emma", "William", "Sophia", "Benjamin", "Ava", "Alexander", "Isabella" };

            var sendingTask = Task.Run(async () =>
            {
                foreach (var name in names)
                {
                    await updateNameRequest.RequestStream.WriteAsync(new Request_Name
                    {
                        Name = name
                    });
                    await Task.Delay(1000); 
                }
                await updateNameRequest.RequestStream.CompleteAsync();
            });

            var receivingTask = Task.Run(async () =>
            {
                while (await updateNameRequest.ResponseStream.MoveNext(cts.Token))
                {
                    Console.WriteLine($"Updated Name: {updateNameRequest.ResponseStream.Current.Name}");
                }
            });

            await Task.WhenAll(sendingTask, receivingTask);


            Console.Read();
        }
    }
}




