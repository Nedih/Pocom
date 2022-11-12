using Pocom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.DAL.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public UserAccount Author { get; set; }
        public IList<Reaction>? Reactions { get; set; } = new List<Reaction>();
        public IList<Comment>? Comments { get; set; } = new List<Comment>();
    }
}
