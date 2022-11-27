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
        public async Task<IdentityResult> CreateAsync(string email, PostDTO item)
        {
            var author = await _userManager.FindByEmailAsync(email);
            //var post = _mapper.Map<Post>(item);
            try
            {
                _repository.Add(new Post { Text = item.Text, Author = author, CreationDate = item.CreationDate,ParentPostId = item.ParentPostId });
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
                _repository.Remove(entity);
        }

        public async Task<PostDTO?> GetPostAsync(Guid id)
        {
            return _mapper.Map<PostDTO>(await _repository.Include(x => x.Author).Include(x=>x.Reactions).FirstOrDefaultAsync(x => x.Id == id));
        }
        public IEnumerable<PostDTO> GetComments(Guid id)
        {
            return _mapper.Map<IEnumerable<PostDTO>>(_repository.Include(x => x.Author).Where(x => x.ParentPostId == id));
        }
        public async Task<IEnumerable<PostDTO>> GetAllAsync(string email)
        {
            var posts = _mapper.Map<IEnumerable<PostDTO>>(_repository.GetAll().Include(x => x.Author));
            if (!string.IsNullOrEmpty(email))
            {
                var user = await _userManager.FindByEmailAsync(email);
                foreach (var post in posts)
                {
                    //post.ReactionStats = _reactionService.GetPostReactions(post.Id);
                    post.UserReactionType = _reactionService.GetUserPostReaction(user.Id, post.Id);
                }
            }
            return posts;
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
            _repository.Update(_mapper.Map<Post>(item));
        }
        public void UpdateText(Guid id,Guid authorId, string text)
        {
            var post = _repository.FirstOrDefault(x => x.Id == id && x.Author.Id == authorId.ToString());
            if (post != null)
            {
                post.Text = text;
                _repository.Update(post);
            }
        }
        private  IEnumerable<PostDTO> Sort(IQueryable<PostDTO> items, string props)
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

        public async Task<IEnumerable<PostDTO>> GetUserReactionsPostsAsync(string email)
        {
            var reactions = await _userManager.Users.Where(x => x.Email == email).Select(x => x.Reactions).FirstOrDefaultAsync();
            if(reactions == null||!reactions.Any()) return new List<PostDTO>();

            var allPosts = _repository.GetAll().Include(x => x.Reactions);
            var intersectedPosts = reactions.Select(x => x.Post).Intersect(allPosts);
            var responsePosts = new List<PostDTO>();
            foreach (var reaction in reactions)
            {
               var post = _repository.Include(x => x.Author).FirstOrDefault(x => x.Id == reaction.PostId);
               if (post != null) 
               {
                    var model = _mapper.Map<PostDTO>(post);
                    //model.ReactionStats = _reactionService.GetPostReactions(post.Id);
                    model.UserReactionType = reaction.Type;
                    responsePosts.Add(model);
               }
            }

            return responsePosts;
        }
    }
}
