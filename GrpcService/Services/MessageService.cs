using Grpc.Core;
using gRPC_MessageServer;
namespace gRPC_Server.Services
{
    public class MessageService :Message.MessageBase
    {
        public override async Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
        {
            string message = $"User: {request.Name}  {Environment.NewLine} Message: {request.Message} {Environment.NewLine}  Date: {DateTime.Now}";
            return new MessageResponse
            {
                Message = message
            };
        }
    }
}