using Pocom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.DAL.Entities
{
    public class Post : IEntity
    {
        Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public UserAccount Author { get; set; }
        public List<Reaction> Reactions { get; set; }   
        public List<Comment> Comments { get; set; }
    }
}
