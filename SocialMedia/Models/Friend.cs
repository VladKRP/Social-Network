using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Friend
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string UserFriendId { get; set; }
        public virtual ApplicationUser UserFriend { get; set; }

        public bool IsConfirmed { get; set; }
    }
}