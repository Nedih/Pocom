using Pocom.BLL.Models;
using Pocom.DAL.Entities;
using Pocom.DAL.Enums;

namespace Pocom.BLL.Interfaces
{
    public interface IReactionService
    {
        public IEnumerable<Reaction> GetAsync(Func<Reaction, bool> predicate);
        public IEnumerable<ReactionDTO> GetUserReactionsAsync(string email);
        public Reaction FirstOrDefaultAsync(Func<Reaction, bool> predicate);
        public Task<bool> CreateAsync(string email, ReactionDTO item);
        public void UpdateAsync(Reaction item);
        public void DeleteAsync(string id);
        Dictionary<ReactionType, int> GetPostReactions(Guid postId);
    }
}
