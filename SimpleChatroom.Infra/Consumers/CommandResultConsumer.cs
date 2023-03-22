using MassTransit;
using Microsoft.AspNetCore.SignalR;
using SimpleChatroom.Infra.Hubs;
using SimpleChatroom.Infra.Messaging.Messages;

namespace SimpleChatroom.Infra.Consumers
{
    public class CommandResultConsumer : IConsumer<CommandResult>
    {
        private readonly IHubContext<ChatRoomHub> _hubContext;

        public CommandResultConsumer(IHubContext<ChatRoomHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<CommandResult> context)
        {
            var result = context.Message;

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", result.Bot, result.Message);
        }
    }
}