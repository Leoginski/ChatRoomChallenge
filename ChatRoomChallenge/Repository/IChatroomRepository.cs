using ChatRoomChallenge.Models;

namespace ChatRoomChallenge.Repository
{
    public interface IChatroomRepository
    {
        IEnumerable<Chatroom> GetFullChatrooms();
    }
}
