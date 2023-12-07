using System;
using System.Collections.Generic;

namespace BlogReview.Models
{
    public partial class MainConBlogHe173248
    {
        public int MainConBlogId { get; set; }
        public int MainConId { get; set; }
        public int BlogId { get; set; }

        public virtual BlogHe173248 Blog { get; set; } = null!;
        public virtual MainContentHe173248 MainCon { get; set; } = null!;
    }
}
