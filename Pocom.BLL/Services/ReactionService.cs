using Pocom.BLL.Interfaces;
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

        public ReactionService(IRepository<Reaction> repository)
        {
            _repository = repository;
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

        public void UpdateAsync(Reaction item)
        {
            _repository.UpdateAndSave(item);
        }

    }
}
