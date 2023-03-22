using SimpleChatroom.Domain.Models;

namespace SimpleChatroom.Domain.Interfaces
{
    public interface IChatroomService
    {
        Task ProcessMessage(string username, string message);

        Task<IEnumerable<Message>> GetMessages();
    }
}