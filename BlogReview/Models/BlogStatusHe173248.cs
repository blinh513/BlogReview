using System;
using System.Collections.Generic;

namespace BlogReview.Models
{
    public partial class BlogStatusHe173248
    {
        public BlogStatusHe173248()
        {
            BlogHe173248s = new HashSet<BlogHe173248>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; } = null!;

        public virtual ICollection<BlogHe173248> BlogHe173248s { get; set; }
    }
}
