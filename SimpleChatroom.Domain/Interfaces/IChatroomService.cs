namespace SimpleChatroom.Domain.Interfaces
{
    public interface IChatroomService
    {
        Task ProcessMessage(string username, string message);
    }
}
