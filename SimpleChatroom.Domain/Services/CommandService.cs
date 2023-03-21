using SimpleChatroom.Domain.Interfaces;
using SimpleChatroom.Domain.Models;
using System.Text.RegularExpressions;

namespace SimpleChatroom.Domain.Services
{
    public class CommandService : ICommandService
    {

        public CommandService()
        {
        }

        public async Task<Command?> ParseCommand(string message)
        {
            var match = Regex.Match(message, @"\/(\w+)\=(\w+)");
            
            if (!match.Success)
                return null;

            return new Command
            {
                CommandName = match.Groups[0].Value,
                Parameter = match.Groups[1].Value
            };
        }

        public Task SendCommand(Command command)
        {
            throw new NotImplementedException();
        }
    }
}
