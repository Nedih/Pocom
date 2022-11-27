using Pocom.BLL.Interfaces;
using Pocom.DAL.Entities;
using Pocom.DAL.Interfaces;
using Pocom.BLL.Extensions;
using AutoMapper;
using Pocom.BLL.Models;
using Microsoft.AspNetCore.Identity;
using Pocom.BLL.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Pocom.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _repository;
        private readonly IMapper _mapper;
        private readonly IReactionService _reactionService;
        private readonly UserManager<UserAccount> _userManager;

        public PostService(IRepository<Post> repository, IMapper mapper, IReactionService reactionService, UserManager<UserAccount> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _reactionService = reactionService;
            _userManager = userManager;
        }
        public async Task<IdentityResult> Create(string email, PostDTO item)
        {
            var author = await _userManager.FindByEmailAsync(email);
            //var post = _mapper.Map<Post>(item);
            try
            {
                _repository.AddAndSave(new Post { Text = item.Text, Author = author, CreationDate = item.CreationDate,ParentPostId = item.ParentPostId });
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Code = ex.TargetSite?.Name, Description = ex.Message });
            }
            //await _repository.SaveAsync();
            return IdentityResult.Success;
        }

        public void Delete(Guid id)
        {
            var entity = _repository.FirstOrDefault(x => x.Id == id);
            if (entity != null)
                _repository.RemoveAndSave(entity);
        }

        public PostDTO? GetPost(Guid id)
        {
            return _mapper.Map<PostDTO>(_repository.Include(x => x.Author).FirstOrDefault(x=>x.Id== id));
        }

        public IEnumerable<PostDTO> Get(Func<Post, bool> predicate)
        {
            return _mapper.Map<IEnumerable<PostDTO>>(_repository.Include(x => x.Author).Where(predicate));
        }
        public async Task<IEnumerable<PostDTO>> GetAll(string email)
        {
            var posts = _mapper.Map<IEnumerable<PostDTO>>(_repository.GetAll().Include(x => x.Author));
            if (string.IsNullOrEmpty(email))
                return posts;
            else
            {
                var userReactedPosts = await GetUserReactionsPosts(email);
                var t = userReactedPosts.UnionBy(posts, x => x.Id);
                var result = new List<PostDTO>();
                return t;
            }
        }
        public IEnumerable<PostDTO> Get(RequestViewModel vm)
        {
            const int pageSize = 5;
            IQueryable<Post> items = _repository.Include(x => x.Author);
            if (vm.Text != null)
            {
                items = _repository
                    .GetWhere(x => x.Text.ToLower().Contains(vm.Text.ToLower()));
            }

            if (vm.Email != null)
            {
                items = _repository
                    .GetWhere(x => x.Author.Email==vm.Email);
            }

            if (vm.SortBy == null)
            {
                return _mapper.Map<IEnumerable<PostDTO>>(items);
            }
            var sortByArray = vm.SortBy.Split(',');
            for (int i = 0; i < sortByArray.Length; i++)
            {
                items = i == 0
                    ? items.OrderBy(sortByArray[i].Replace(" desc", ""), sortByArray[i].EndsWith("desc"))
                    : items.ThenBy(sortByArray[i].Replace(" desc", ""), sortByArray[i].EndsWith("desc"));
            }
            return _mapper.Map<IEnumerable<PostDTO>>(items.Paginate(vm.Page,pageSize));
        }

        public void Update(PostDTO item)
        {
            _repository.UpdateAndSave(_mapper.Map<Post>(item));
        }
        public void UpdateText(Guid id,Guid authorId, string text)
        {
            var post = _repository.FirstOrDefault(x => x.Id == id && x.Author.Id == authorId.ToString());
            if (post != null)
            {
                post.Text = text;
                _repository.UpdateAndSave(post);
            }
        }
        public  IEnumerable<PostDTO> Sort(IQueryable<PostDTO> items, string props)
        {
            if (props == null)
            {
                return _mapper.Map<IEnumerable<PostDTO>>(items).AsQueryable();
            }
            var sortByArray = props.Split(',');
            for (int i = 0; i < sortByArray.Length; i++)
            {
                items = i == 0
                    ? items.OrderBy(sortByArray[i].Replace(" desc", ""), sortByArray[i].EndsWith("desc"))
                    : items.ThenBy(sortByArray[i].Replace(" desc", ""), sortByArray[i].EndsWith("desc"));
            }
            return items;
        }

        public async Task<IEnumerable<PostDTO>> GetUserReactionsPosts(string email)
        {
            var reactions = await _userManager.Users.Where(x => x.Email == email).Select(x => x.Reactions).FirstOrDefaultAsync();
            var allPosts = _repository.GetAll().Include(x => x.Reactions);
            var result2 = reactions?.Select(x => x.Post).Intersect(allPosts);
            var result = new List<PostDTO>();
            foreach (var reaction in reactions)
            {
               var post = _repository.Include(x => x.Author).FirstOrDefault(x => x.Id == reaction.PostId);
               if (post != null) 
               {
                    var model = _mapper.Map<PostDTO>(post);
                    model.ReactionStats = _reactionService.GetPostReactions(post.Id);
                    model.UserReactionType = reaction.Type;
                    result.Add(model);
               }
            }

            return result;
        }
    }
}
