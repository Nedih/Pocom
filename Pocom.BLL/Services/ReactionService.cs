using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pocom.BLL.Interfaces;
using Pocom.BLL.Models;
using Pocom.DAL.Entities;
using Pocom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void CreateAsync(Reaction item)
        {
            _repository.AddAndSave(item);
        }

        public void DeleteAsync(string id)
        {
            var entity = _repository.FirstOrDefault(x => x.Id.ToString() == id);
            if (entity != null)
                _repository.RemoveAndSave(entity);
        }

        public Reaction FirstOrDefaultAsync(Func<Reaction, bool> predicate)
        {
            return _repository.FirstOrDefault(predicate);
        }

        public IEnumerable<Reaction> GetAsync(Func<Reaction, bool> predicate)
        {
            return _repository.GetWhere(predicate).ToList();
        }

        public async Task<IEnumerable<ReactionDTO>> GetUserReactionsAsync(string email)
        {
            var user = await _userManager.Users.Include(x => x.Reactions).FirstOrDefaultAsync(x => x.Email == email);
            //_userManager.FindByEmailAsync(email).;
            var reactions = user?.Reactions?.ToList();
            return _mapper.Map<IEnumerable<ReactionDTO>>(user?.Reactions?.ToList()).AsQueryable();
        }

        public void UpdateAsync(Reaction item)
        {
            _repository.UpdateAndSave(item);
        }

    }
}
