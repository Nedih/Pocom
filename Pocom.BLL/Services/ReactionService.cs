using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pocom.BLL.Interfaces;
using Pocom.BLL.Models;
using Pocom.DAL.Entities;
using Pocom.DAL.Enums;
using Pocom.DAL.Interfaces;

namespace Pocom.BLL.Services
{
    public class ReactionService : IReactionService
    {
        private readonly IRepository<Reaction> _repository;
        private readonly UserManager<UserAccount> _userManager;
        private readonly IMapper _mapper;

        public ReactionService(IRepository<Reaction> repository, UserManager<UserAccount> userManager, IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(string email, ReactionDTO item)
        {
            var user = await _userManager.FindByEmailAsync(email);
            Reaction t = _repository.FirstOrDefault(x => x.AuthorId == user.Id && x.PostId == item.PostId);
            if (t == null) 
            {
                //_repository.AddAndSave(_mapper.Map<Reaction>(item));
                var r = new Reaction { Author = user, PostId = item.PostId, Type = item.Type };
                _repository.AddAndSave(r);
                return true;
            }
            return false;
        }

        public void Delete(string id)
        {
            var entity = _repository.FirstOrDefault(x => x.Id.ToString() == id);
            if (entity != null)
                _repository.RemoveAndSave(entity);
        }

        public Reaction FirstOrDefault(Func<Reaction, bool> predicate)
        {
            return _repository.FirstOrDefault(predicate);
        }

        public IEnumerable<Reaction> Get(Func<Reaction, bool> predicate)
        {
            return _repository.GetWhere(predicate).ToList();
        }

        public IEnumerable<ReactionDTO> GetUserReactions(string email)
        {
            var reactions = _userManager.Users.Where(x => x.Email == email).Select(x => x.Reactions ).FirstOrDefault();
            return _mapper.Map<IEnumerable<ReactionDTO>>(reactions);
        }

        public void Update(Reaction item)
        {
            _repository.UpdateAndSave(item);
        }

        public Dictionary<ReactionType, int> GetPostReactions(Guid postId)
        {
            var result = new Dictionary<ReactionType, int>();
            foreach (ReactionType reaction in Enum.GetValues(typeof(ReactionType)))
            {
                result.Add(reaction, _repository.Count(x => x.Type == reaction && x.PostId == postId));
            }
            return result;
        }
    }
}
