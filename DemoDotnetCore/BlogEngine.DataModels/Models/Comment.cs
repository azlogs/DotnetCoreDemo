using System;

namespace BlogEngine.DataModels.Models
{
    public partial class Comment : BaseEntity
    {
        public Guid? ParrentCommentId { get; set; }
        public Guid UserId { get; set; }
        public string CommentContent { get; set; }
        public Guid PostId { get; set; }
        public bool IsDelete { get; set; } 

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
