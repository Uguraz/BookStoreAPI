using BookStore.BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookStore.API.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}