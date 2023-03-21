using System.ComponentModel.DataAnnotations;

namespace ChatRoomChallenge.Models
{
    public class Chatroom
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
