using SimpleChatroom.Domain.Interfaces;
using SimpleChatroom.Domain.Models;

namespace SimpleChatroom.Domain.Services
{
    public class ChatroomService : IChatroomService
    {
        private readonly IChatroomRepository _repository;
        private readonly ICommandService _commandService;

        public ChatroomService(IChatroomRepository repository, ICommandService commandService)
        {
            _repository = repository;
            _commandService = commandService;
        }

        public async Task ProcessMessage(string userId, string messageText)
        {
            var message = Message.Create(userId, messageText);

            var command = await _commandService.ParseCommand(messageText);

            if (command is null)
                await _repository.AddMessage(message);
            else
                await _commandService.SendCommand(command);            
        }
    }
}
