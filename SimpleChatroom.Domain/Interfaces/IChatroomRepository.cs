using SimpleChatroom.Domain.Models;

namespace SimpleChatroom.Domain.Interfaces
{
    public interface IChatroomRepository
    {
        Task AddMessage(Message message);
        Task<IEnumerable<Message>> GetMessages();
    }
}