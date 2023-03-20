using Microsoft.AspNetCore.SignalR;

namespace ChatRoomChallenge.Hubs
{
    public class ChatRoom : Hub
    {
        public async Task SendMessage(string username, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", username, message);
        }
    }
}
