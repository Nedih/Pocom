using Pocom.BLL.Models;
using Pocom.DAL.Entities;
using Pocom.DAL.Enums;

namespace Pocom.BLL.Interfaces
{
    public interface IReactionService
    {
        public IEnumerable<ReactionDTO> Get(Func<Reaction, bool> predicate);
        public IEnumerable<ReactionDTO> GetUserReactions(string email);
        public ReactionDTO FirstOrDefault(Func<Reaction, bool> predicate);
        public Task<bool> CreateAsync(string email, ReactionDTO item);
        public void Update(ReactionDTO item);
        public void Delete(string id);
        Dictionary<ReactionType, int> GetPostReactions(Guid postId);
    }
}
