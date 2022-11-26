using Pocom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pocom.DAL.Entities.Reaction;

namespace Pocom.BLL.Models
{
    public class ReactionDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PostId { get; set; }
        public string AuthorId { get; set; }
        public ReactionType Type { get; set; }
        //public UserAccount Author { get; set; }
        //public Post Post { get; set; }
    }
}
