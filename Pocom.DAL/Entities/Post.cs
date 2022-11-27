namespace Pocom.DAL.Entities
{
    public class Post
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Image { get; set; } 
        public UserAccount Author { get; set; }
        public Guid? ParentPostId { get; set; }
        public Post? ParentPost { get; set; }
        public IList<Reaction>? Reactions { get; set; } = new List<Reaction>();
    }
}
