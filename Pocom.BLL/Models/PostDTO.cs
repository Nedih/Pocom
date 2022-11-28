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
        public ReactionType? UserReactionType { get; set; }
        public Dictionary<ReactionType, int>? ReactionStats
        {
            get
            {
                var result = new Dictionary<ReactionType, int>();
                foreach (ReactionType reaction in Enum.GetValues(typeof(ReactionType)))
                {
                    result.Add(reaction, Reactions.Count(x => x.Type == reaction));
                }
                return result;
            }
        }
        public IList<ReactionDTO>? Reactions { get; set; } = new List<ReactionDTO>();

        public ReactionType? GetUserReactionType(string? userId = "")
        {
            return Reactions?.FirstOrDefault(x => x.AuthorId == userId)?.Type;
        }
    }
}
