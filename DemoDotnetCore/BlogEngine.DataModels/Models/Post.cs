using System;
using System.Collections.Generic;

namespace BlogEngine.DataModels.Models
{
    public partial class Post : BaseEntity
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public string Title { get; set; }
        public string SlugTitle { get; set; }
        public string PostContent { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public bool IsDelete { get; set; } 
        public virtual User CreatedUser { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
