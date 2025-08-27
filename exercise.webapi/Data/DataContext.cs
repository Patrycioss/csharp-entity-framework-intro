using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("Library");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var seeder = new Seeder();

            modelBuilder.Entity<Author>().HasData(seeder.Authors);
            modelBuilder.Entity<Book>().HasData(seeder.Books);
            modelBuilder.Entity<Publisher>().HasData(seeder.Publishers);
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
    }
}
