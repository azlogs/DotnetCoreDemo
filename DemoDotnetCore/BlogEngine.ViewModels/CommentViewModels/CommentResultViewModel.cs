using System;
using System.Collections.Generic;

namespace BlogEngine.ViewModels.CommentViewModels
{
    public class CommentResultViewModel
    {
        public CommentResultViewModel()
        {
            Comments = new List<CommentResultViewModel>();
        }

        public Guid UserId { get; set; }
        public string CommentContent { get; set; }
        public Guid PostId { get; set; }

        List<CommentResultViewModel> Comments;
    }
}