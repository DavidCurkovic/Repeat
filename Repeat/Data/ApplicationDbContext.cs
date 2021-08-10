using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repeat.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Repeat.Models.Event> Event { get; set; }
        public DbSet<Repeat.Models.Club> Club { get; set; }
        public DbSet<Repeat.Models.Image> Image { get; set; }
    }
}
