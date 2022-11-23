using Pocom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Models
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public byte[]? Image { get; set; }
        public string Author { get; set; }
        public IList<Reaction>? Reactions { get; set; } = new List<Reaction>();
        public IList<Post>? Comments { get; set; } = new List<Post>();
    }
}
