using System.ComponentModel.DataAnnotations;

namespace BlogEngine.ViewModels.PostViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string SlugTitle { get; set; }

        [Required]
        [MinLength(3)]
        public string PostContent { get; set; }

        public string Author { get; set; }

        public string Tags { get; set; }
    }
}