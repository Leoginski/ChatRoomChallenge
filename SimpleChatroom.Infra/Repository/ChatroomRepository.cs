using Microsoft.EntityFrameworkCore;
using SimpleChatroom.Domain.Interfaces;
using SimpleChatroom.Domain.Models;
using SimpleChatroom.Infra.Context;

namespace SimpleChatroom.Repository
{
    public class ChatroomRepository : IChatroomRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatroomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMessage(Message message)
        {
            await _context.Messages
                .AddAsync(message);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetMessages()
        {
            return _context.Messages
                .Include(x => x.User)
                .OrderBy(x => x.Date);
        }
    }
}