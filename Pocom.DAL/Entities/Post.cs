using System.ComponentModel.DataAnnotations;

namespace Pocom.DAL.Entities
{
    public class Post
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public string? Image { get; set; }
        [Required]
        public string AuthorId { get; set; }
        [Required]
        public UserAccount Author { get; set; }
        public Guid? ParentPostId { get; set; }
        public Post? ParentPost { get; set; }
        public IList<Post>? Comments { get; set; } = new List<Post>();
        public IList<Reaction>? Reactions { get; set; } = new List<Reaction>();       
    }
}
