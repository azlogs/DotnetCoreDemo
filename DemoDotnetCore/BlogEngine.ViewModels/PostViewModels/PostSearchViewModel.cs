using System;

namespace BlogEngine.ViewModels.PostViewModels
{
    public class PostSearchViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
    }
}