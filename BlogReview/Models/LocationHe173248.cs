using System;
using System.Collections.Generic;

namespace BlogReview.Models
{
    public partial class LocationHe173248
    {
        public LocationHe173248()
        {
            LocaBlogHe173248s = new HashSet<LocaBlogHe173248>();
        }

        public int LocaId { get; set; }
        public string LocaContent { get; set; } = null!;

        public virtual ICollection<LocaBlogHe173248> LocaBlogHe173248s { get; set; }
    }
}
