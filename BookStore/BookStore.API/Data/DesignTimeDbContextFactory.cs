using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BookStore.API.Data;
using BookStore.BookStore.API.Data;


namespace BookStore.API.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BookStoreContext>
    {
        public BookStoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookStoreContext>();
            optionsBuilder.UseSqlite("Data Source=bookstore.db");

            return new BookStoreContext(optionsBuilder.Options);
        }
    }
}