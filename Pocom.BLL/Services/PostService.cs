using Pocom.BLL.Interfaces;
using Pocom.DAL.Entities;
using Pocom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pocom.BLL.Extensions;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Pocom.BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace Pocom.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<UserAccount> _userManager;

        public PostService(IRepository<Post> repository, AutoMapper.IMapper mapper, UserManager<UserAccount> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IdentityResult> CreateAsync(string email, PostDTO item)
        {
            var author = await _userManager.FindByEmailAsync(email);
            //var post = _mapper.Map<Post>(item);
            try
            {
                _repository.AddAndSave(new Post { Text = item.Text, Author = author, CreationDate = item.CreationDate });
            }
            catch (Exception ex) {
                return IdentityResult.Failed(new IdentityError { Code = ex.TargetSite.Name, Description = ex.Message});
            }
            //await _repository.SaveAsync();
            return IdentityResult.Success;
        }

        public void DeleteAsync(Guid id)
        {
            var entity = _repository.FirstOrDefault(x => x.Id == id);
            if (entity != null)
                _repository.RemoveAndSave(entity);
        }

        public PostDTO? FirstOrDefaultAsync(Func<Post, bool> predicate)
        {
            return _mapper.Map<PostDTO>(_repository.Include(x => x.Author).FirstOrDefault(predicate));
        }

        public IQueryable<PostDTO> GetAsync(Func<Post, bool> predicate)
        {
            return _mapper.Map<IEnumerable<PostDTO>>(_repository.Include(x => x.Author).Where(predicate)).AsQueryable<PostDTO>();
        }

        public void UpdateAsync(Post item)
        {
            _repository.UpdateAndSave(item);
        }
        public IQueryable<PostDTO> Sort(IQueryable<PostDTO> items,string props)
        {
            if (props==null)
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
            return _mapper.Map<IQueryable<PostDTO>>(items);
        }

    }
}
