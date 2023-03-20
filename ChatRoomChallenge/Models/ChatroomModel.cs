namespace ChatRoomChallenge.Models
{
    public class ChatroomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<MessageModel> Messages { get; set; }
    }
}
