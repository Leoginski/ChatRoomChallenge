using MassTransit;
using Microsoft.AspNetCore.SignalR;
using SimpleChatroom.Domain.Models;
using SimpleChatroom.Hubs;

namespace SimpleChatroom.Consumer
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
