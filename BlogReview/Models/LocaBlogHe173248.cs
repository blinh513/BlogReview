using System;
using System.Collections.Generic;

namespace BlogReview.Models
{
    public partial class LocaBlogHe173248
    {
        public int LocalBlogId { get; set; }
        public int LocaId { get; set; }
        public int BlogId { get; set; }

        public virtual BlogHe173248 Blog { get; set; } = null!;
        public virtual LocationHe173248 Loca { get; set; } = null!;
    }
}
