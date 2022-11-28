using Pocom.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Models.ViewModels
{
    public class ReactionViewModel
    {
        public Guid PostId { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}
