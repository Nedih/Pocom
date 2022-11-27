using Pocom.DAL.Entities;
using Pocom.DAL.Enums;

namespace Pocom.BLL.Models
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? Image { get; set; }
        public string? Author { get; set; }
        public string? AuthorImage { get; set; }
        public Guid? ParentPostId { get; set; }
        public Dictionary<ReactionType, int>? ReactionStats { get; set; }
        public IList<Reaction>? Reactions { get; set; } = new List<Reaction>();
    }
}
