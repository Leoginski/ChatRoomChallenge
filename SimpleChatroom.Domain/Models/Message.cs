using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleChatroom.Domain.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public DateTime Date { get; private set; }

        public static Message Create(string userId, string text)
        {
            return new Message
            {
                UserId = userId,
                Text = text,
                Date = DateTime.Now
            };
        }

        public virtual string GetUserName()
        {
            return this.User.UserName;
        }
    }
}