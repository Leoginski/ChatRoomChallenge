using ChatRoomChallenge.Repository;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoomChallenge.Hubs
{
    public class ChatRoomHub : Hub
    {
        public ChatRoomHub(IChatroomRepository service)
        {
            
        }

        public async Task SendMessage(string username, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", username, message);
        }
    }
}
