using ChatRoomChallenge.Data;
using ChatRoomChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatRoomChallenge.Repository
{
    public class ChatroomRepository : IChatroomRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatroomRepository(ApplicationDbContext context)
        {
            _context = context;    
        }

        public IEnumerable<Chatroom> GetFullChatrooms()
        {
            return _context.Chatrooms
                .Include(x => x.Messages)
                .Include("Messages.User");
        }
    }
}
