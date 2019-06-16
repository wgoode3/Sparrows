using Microsoft.EntityFrameworkCore;
using Sparrows.Models;

namespace Sparrows.Models {
    public class DBContext : DbContext {
        public DBContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlite("Filename=mydb.db");
        }
        public DbSet<Note> Notes {get;set;}

    }
}