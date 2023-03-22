using MassTransit;
using SimpleChatroom.Consumer;
using SimpleChatroom.Domain.Models;

namespace SimpleChatroom.Worker.Consumer
{
    public class CommandConsumer : IConsumer<Command>
    {
        private IBus _bus;

        public CommandConsumer(IBus bus)
        {
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<Command> context)
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:simplechatroom-result"));

            await endpoint.Send(new CommandResult { Bot = "StockBot", Message = "LALALALA"});
        }
    }
}
