using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatRoomChallenge.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey("ChatRoomModel")]
        public string ChatRoomId { get; set; } = null!;

        public Chatroom ChatRoom { get; set; }
    }
}