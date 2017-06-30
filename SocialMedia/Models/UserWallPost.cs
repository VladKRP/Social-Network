using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class UserWallPost
    {

        public int Id { get; set;}

        public string Text { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public DateTime PostedDate { get; set; }

    }
}