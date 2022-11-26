using Pocom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.DAL.Entities
{
    public class Reaction
    {
        public enum ReactionType
        {
            Like,
            Fire,
            Dislike
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public ReactionType Type { get; set; }
        public string AuthorId { get; set; }
        public Guid PostId { get; set; }
        public UserAccount Author { get; set; }
        public Post Post { get; set; }
    }
}
