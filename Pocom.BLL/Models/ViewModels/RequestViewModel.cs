using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Models.ViewModels
{
    public class RequestViewModel
    {
        public int Page { get; set; } = 1;
        public string? Text { get; set; }
        public string? SortBy { get; set; }
        public string? Email { get; set; }
    }
}
