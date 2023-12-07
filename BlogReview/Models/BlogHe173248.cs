using System;
using System.Collections.Generic;

namespace BlogReview.Models
{
    public partial class BlogHe173248
    {
        public BlogHe173248()
        {
            CommentHe173248s = new HashSet<CommentHe173248>();
            LocaBlogHe173248s = new HashSet<LocaBlogHe173248>();
            MainConBlogHe173248s = new HashSet<MainConBlogHe173248>();
            RateHe173248s = new HashSet<RateHe173248>();
            TagDraftHe173248s = new HashSet<TagDraftHe173248>();
        }

        public int BlogId { get; set; }
        public string BlogName { get; set; } = null!;
        public string BlogDescription { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string ImageBlog { get; set; } = null!;
        public DateTime CreateOn { get; set; }
        public DateTime? ModifiOn { get; set; }
        public double AverRate { get; set; }
        public string AverRateFormatted
        {
            get
            {
                return AverRate.ToString("0.00"); // Định dạng thành chuỗi với 2 chữ số thập phân
            }
        }
        public int StatusId { get; set; }
        public int UserId { get; set; }

        public virtual BlogStatusHe173248 Status { get; set; } = null!;
        public virtual UserHe173248 User { get; set; } = null!;
        public virtual ICollection<CommentHe173248> CommentHe173248s { get; set; }
        public virtual ICollection<LocaBlogHe173248> LocaBlogHe173248s { get; set; }
        public virtual ICollection<MainConBlogHe173248> MainConBlogHe173248s { get; set; }
        public virtual ICollection<RateHe173248> RateHe173248s { get; set; }
        public virtual ICollection<TagDraftHe173248> TagDraftHe173248s { get; set; }
    }
}
