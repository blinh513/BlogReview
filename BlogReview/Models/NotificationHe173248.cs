using System;
using System.Collections.Generic;

namespace BlogReview.Models
{
    public partial class NotificationHe173248
    {
        public int NotifId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreateOn { get; set; }
        public bool IsRead { get; set; }
        public int UserId { get; set; }

        public virtual UserHe173248 User { get; set; } = null!;
    }
}
