using System;
using System.Collections.Generic;

namespace BlogReview.Models
{
    public partial class TagDraftHe173248
    {
        public int TagDraftId { get; set; }
        public string? LocationDraft { get; set; }
        public string? CategoryDraft { get; set; }
        public int BlogId { get; set; }

        public virtual BlogHe173248 Blog { get; set; } = null!;
    }
}
