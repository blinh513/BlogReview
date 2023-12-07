using System;
using System.Collections.Generic;

namespace BlogReview.Models
{
    public partial class CommentHe173248
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
        public int BlogId { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime ModifiOn { get; set; }

        public virtual BlogHe173248 Blog { get; set; } = null!;
        public virtual UserHe173248 User { get; set; } = null!;
    }
}
