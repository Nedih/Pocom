using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Pocom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.DAL
{
    public class PocomContext : IdentityDbContext
    {
        public PocomContext() : base() { }
    }
}
