using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Egzamin_Rectangle1.Models;

namespace Egzamin_Rectangle1.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Rectangle> Rectangle { get; set; } = default!;

        public DbSet<Unit> Units { get; set; } = default!;
    }
}
