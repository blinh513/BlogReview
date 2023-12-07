using System;
using System.Collections.Generic;

namespace BlogReview.Models
{
    public partial class UserHe173248
    {
        public UserHe173248()
        {
            BlogHe173248s = new HashSet<BlogHe173248>();
            CommentHe173248s = new HashSet<CommentHe173248>();
            NotificationHe173248s = new HashSet<NotificationHe173248>();
            RateHe173248s = new HashSet<RateHe173248>();
        }

        public int UserId { get; set; }
        public string NameDisplay { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ImageUser { get; set; }
        public int RoleId { get; set; }

        public virtual RoleHe173248 Role { get; set; } = null!;
        public virtual ICollection<BlogHe173248> BlogHe173248s { get; set; }
        public virtual ICollection<CommentHe173248> CommentHe173248s { get; set; }
        public virtual ICollection<NotificationHe173248> NotificationHe173248s { get; set; }
        public virtual ICollection<RateHe173248> RateHe173248s { get; set; }
    }
}
