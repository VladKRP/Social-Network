using System.Collections.Generic;

namespace SocialMedia.Models
{
    public class Dialog
    {

        public Dialog()
        {
            Messages = new List<UserMessage>();
        }

        public int Id { get; set; }

        public string SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }
        public virtual ApplicationUser Receiver { get; set; }

        public virtual ICollection<UserMessage> Messages { get; set; }
    }
}