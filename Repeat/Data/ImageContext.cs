using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repeat.Models;

namespace Repeat.Data
{
    public class ImageContext : DbContext
    {
        public ImageContext (DbContextOptions<ImageContext> options)
            : base(options)
        {
        }

        public DbSet<Repeat.Models.Image> Image { get; set; }
    }
}
