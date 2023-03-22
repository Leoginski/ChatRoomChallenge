using MassTransit;
using SimpleChatroom.Domain.Commands;
using SimpleChatroom.Infra.Messaging.Messages;
using SimpleChatroom.Worker.Services;

namespace SimpleChatroom.Worker.Consumer
{
    public class CommandConsumer : IConsumer<Command>
    {
        private IBus _bus;
        private IStockService _stockService;

        public CommandConsumer(IBus bus, IStockService stockService)
        {
            _bus = bus;
            _stockService = stockService;
        }

        public async Task Consume(ConsumeContext<Command> context)
        {
            var stock = await _stockService.GetStockData(context.Message.Parameter);

            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:simplechatroom-result"));

            await endpoint.Send(new CommandResult { Bot = "StockBot", Message = $"{stock.Symbol} quote is ${stock.Open} per share" });
        }
    }
}