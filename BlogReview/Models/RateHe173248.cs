using System;
using System.Collections.Generic;

namespace BlogReview.Models
{
    public partial class RateHe173248
    {
        public int RateId { get; set; }
        public int Number { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }

        public virtual BlogHe173248 Blog { get; set; } = null!;
        public virtual UserHe173248 User { get; set; } = null!;
    }
}
