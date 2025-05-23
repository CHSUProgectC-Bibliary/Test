using BookReviewAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookReviewAPI.Data
{
    public class ApplicationDbContext(DbContextOptions options, IConfiguration configuration) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BooksReviewDb;Username=postgres;Password=559330");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
