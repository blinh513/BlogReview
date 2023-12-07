using System;
using System.Collections.Generic;

namespace BlogReview.Models
{
    public partial class RoleHe173248
    {
        public RoleHe173248()
        {
            UserHe173248s = new HashSet<UserHe173248>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<UserHe173248> UserHe173248s { get; set; }
    }
}
