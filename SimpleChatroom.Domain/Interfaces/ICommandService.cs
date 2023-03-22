using SimpleChatroom.Domain.Commands;

namespace SimpleChatroom.Domain.Interfaces
{
    public interface ICommandService
    {
        Task<Command?> ParseCommand(string message);
        Task SendCommand(Command command);
    }
}
