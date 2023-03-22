using MassTransit;
using SimpleChatroom.Domain.Commands;
using SimpleChatroom.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace SimpleChatroom.Domain.Services
{
    public class CommandService : ICommandService
    {
        private readonly IBus _bus;

        public CommandService(IBus bus)
        {
            _bus = bus;
        }

        public async Task<Command?> ParseCommand(string message)
        {
            var match = Regex.Match(message, @"\/(\w+)\=(.*)");

            if (!match.Success)
                return null;

            return new Command
            {
                CommandName = match.Groups[1].Value,
                Parameter = match.Groups[2].Value
            };
        }

        public async Task SendCommand(Command command)
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:simplechatroom-command"));

            await endpoint.Send(command, CancellationToken.None);
        }
    }
}