using System;
using System.Collections.Generic;

namespace BlogReview.Models
{
    public partial class MainContentHe173248
    {
        public MainContentHe173248()
        {
            MainConBlogHe173248s = new HashSet<MainConBlogHe173248>();
        }

        public int MainConId { get; set; }
        public string MainContent { get; set; } = null!;

        public virtual ICollection<MainConBlogHe173248> MainConBlogHe173248s { get; set; }
    }
}
