using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.DAL.Entities
{
    public class Reaction
    {
        public Guid Id { get; set; }
        public UserAccount Author { get; set; }
        public Post Post { get; set; }
    }
}
