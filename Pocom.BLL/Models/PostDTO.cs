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
        public string? AuthorLogin { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorImage { get; set; }
        public Guid? ParentPostId { get; set; }
        public ReactionType? UserReactionType { get; set; }
        public Dictionary<ReactionType, int?>? ReactionStats { get; set; }
        public int? CommentsCount { get; set; }
        //public IList<ReactionDTO>? Reactions { get; set; } = new List<ReactionDTO>();

        public static Dictionary<ReactionType, int?>? GetReactionStats(IList<Reaction>? reactions)
        {
            if (reactions != null) { 
                var result = new Dictionary<ReactionType, int?>();
                foreach (ReactionType reaction in Enum.GetValues(typeof(ReactionType)))
                {
                    result.Add(reaction, reactions?.Count(x => x.Type == reaction));
                }
                return result;
            }
            else return null;
        }
        public static ReactionType? GetUserReactionType(IList<Reaction>? reactions, string? userId = "")
        {
            if(reactions != null)
                return reactions?.FirstOrDefault(x => x.AuthorId == userId)?.Type;
            else return null;
        }
    }
}
