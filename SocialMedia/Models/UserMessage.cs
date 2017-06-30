using System;

namespace SocialMedia.Models
{
    public class UserMessage
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }
        public virtual ApplicationUser Receiver { get; set; }

        public DateTime PostedDate { get; set; }
    }
}