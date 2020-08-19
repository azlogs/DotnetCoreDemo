using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.ViewModels.CommentViewModels
{
    public class CommentViewModel
    {
        public Guid CommentId { get; set; }

        public Guid UserId { get; set; }
        public string CommentContent { get; set; }
        public Guid PostId { get; set; }
    }
}