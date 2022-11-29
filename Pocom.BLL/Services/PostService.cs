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

        public async Task<PostDTO?> GetPostAsync(Guid id, string? userId = "")
        {
            return _mapper.Map<PostDTO>(await _repository.Include(x => x.Author).Include(x=>x.Reactions).FirstOrDefaultAsync(x => x.Id == id), opt => opt.Items["userId"] = userId);
        }
        public IEnumerable<PostDTO> GetComments(Guid id, string? userId = "")
        {
            return _mapper.Map<IEnumerable<PostDTO>>(_repository.Include(x => x.Author).Where(x => x.ParentPostId == id), opt => opt.Items["userId"] = userId);
        }
        public IEnumerable<PostDTO> GetAll(string? userId = "")
        {
            var posts = _mapper.Map<IEnumerable<PostDTO>>(_repository.GetAll().Include(x => x.Author).Include(x => x.Reactions),
                opt => opt.Items["userId"] = userId);
            return posts;
        }
        public IEnumerable<PostDTO> Get(RequestViewModel vm)
        {
            const int pageSize = 5;
            IQueryable<Post> items = _repository.Include(x => x.Author);
            if (vm.Text != null)
            {
                items = _repository
                    .Include(x => x.Author)
                    .Include(x => x.Reactions)
                    .Where(x => x.Text.ToLower().Contains(vm.Text.ToLower()));
            }

            if (vm.Id != null)
            {
                items = _repository
                    .Include(x => x.Author)
                    .Include(x => x.Reactions)
                    .Where(x => x.AuthorId == vm.Id);
            }

            if (vm.SortBy == null)
            {
                return _mapper.Map<IEnumerable<PostDTO>>(items, opt => opt.Items["userId"] = vm.Id);
            }
            var sortByArray = vm.SortBy.Split(',');
            for (int i = 0; i < sortByArray.Length; i++)
            {
                items = i == 0
                    ? items.OrderBy(sortByArray[i].Replace(" desc", ""), sortByArray[i].EndsWith("desc"))
                    : items.ThenBy(sortByArray[i].Replace(" desc", ""), sortByArray[i].EndsWith("desc"));
            }
            return _mapper.Map<IEnumerable<PostDTO>>(items.Paginate(vm.Page,pageSize), opt => opt.Items["userId"] = vm.Id);
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

        public async Task<IEnumerable<PostDTO>> GetUserReactionsPostsAsync(string userId)
        {
            var reactions = await _userManager.Users.Where(x => x.Id == userId).Select(x => x.Reactions).FirstOrDefaultAsync();
            if (reactions == null || !reactions.Any()) return new List<PostDTO>();
            var allPosts = _repository.GetAll().Include(x => x.Reactions).Include(x => x.Author);
            var intersectedPosts = reactions.Select(x => x.Post).Intersect(allPosts);
            var posts = _mapper.Map<IEnumerable<PostDTO>>(intersectedPosts,
                opt => opt.Items["userId"] = userId);
            return posts;
        }
    }
}
