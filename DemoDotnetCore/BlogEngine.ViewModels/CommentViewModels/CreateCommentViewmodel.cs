using System;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.ViewModels.CommentViewModels
{
    public class CreateCommentViewmodel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string CommentContent { get; set; }

        [Required]
        public Guid PostId { get; set; }
    }
}