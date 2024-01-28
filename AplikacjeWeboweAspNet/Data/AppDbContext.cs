using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplikacjeWeboweAspNet.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace AplikacjeWeboweAspNet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}

