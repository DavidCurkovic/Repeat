using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repeat.Models;

namespace Repeat.Data
{
    public class EventContext : DbContext
    {
        public EventContext (DbContextOptions<EventContext> options)
            : base(options)
        {
        }

        public DbSet<Repeat.Models.Event> Event { get; set; }
        public DbSet<Repeat.Models.Club> Club { get; set; }
        public DbSet<Repeat.Models.Image> Image { get; set; }
    }
}
