using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SimpleChatroom.Domain.Interfaces;

namespace SimpleChatroom.Hubs
{
    public class ChatRoomHub : Hub
    {
        private readonly IChatroomService _service;

        public ChatRoomHub(IChatroomService service)
        {
            _service = service;
        }

        public async Task SendMessage(string userId, string userName, string messageText)
        {
            await _service.ProcessMessage(userId, messageText);

            await Clients.All.SendAsync("ReceiveMessage", userName, messageText);
        }
    }
}
