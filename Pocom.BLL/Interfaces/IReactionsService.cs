using Pocom.BLL.Models;
using Pocom.BLL.Models.ViewModels;
using Pocom.DAL.Entities;
using Pocom.DAL.Enums;

namespace Pocom.BLL.Interfaces
{
    public interface IReactionService
    {
        public IEnumerable<ReactionDTO> GetUserReactions(string authorId);
        public bool Create(string authorId, ReactionDTO item);
        public void Update(string authorId, ReactionViewModel model);
        public void Delete(string authorId, ReactionViewModel model);
        Dictionary<ReactionType, int> GetPostReactions(Guid postId);
        ReactionType? GetUserPostReaction(string userId, Guid postId);
    }
}
