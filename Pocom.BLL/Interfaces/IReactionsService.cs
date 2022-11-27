using Pocom.BLL.Models;
using Pocom.BLL.Models.ViewModels;
using Pocom.DAL.Entities;
using Pocom.DAL.Enums;

namespace Pocom.BLL.Interfaces
{
    public interface IReactionService
    {
        public IEnumerable<ReactionDTO> GetUserReactions(string email);
        public Task<bool> CreateAsync(string email, ReactionDTO item);
        public void Update(ReactionViewModel model);
        public void Delete(ReactionViewModel model);
        Dictionary<ReactionType, int> GetPostReactions(Guid postId);
        ReactionType? GetUserPostReaction(string userId, Guid postId);
    }
}
