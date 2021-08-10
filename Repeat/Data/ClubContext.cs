using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repeat.Models;

namespace Repeat.Data
{
    public class ClubContext : DbContext
    {
        public ClubContext (DbContextOptions<ClubContext> options)
            : base(options)
        {
        }

        public DbSet<Repeat.Models.Club> Club { get; set; }
    }
}
