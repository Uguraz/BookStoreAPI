using BookStore.BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookStore.API.Data
{
    public static class DbInitializer
    {
        public static void Seed(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BookStoreContext>();
            
            context.Database.EnsureDeleted();

            context.Database.Migrate();

            if (!context.Books.Any())
            {
                var authors = new[]
                {
                    "J.K. Rowling", "George R.R. Martin", "J.R.R. Tolkien", "Agatha Christie", "Stephen King",
                    "Jane Austen", "Isaac Asimov", "Ernest Hemingway", "Mark Twain", "Charles Dickens",
                    "F. Scott Fitzgerald", "Leo Tolstoy", "H.G. Wells", "Arthur Conan Doyle", "Dan Brown",
                    "Emily Brontë", "Mary Shelley", "Oscar Wilde", "Virginia Woolf", "J.D. Salinger",
                    "John Steinbeck", "Haruki Murakami", "Neil Gaiman", "Brandon Sanderson", "Terry Pratchett",
                    "Suzanne Collins", "Rick Riordan", "Margaret Atwood", "Colleen Hoover", "James Patterson"
                };

                var titles = new[]
                {
                    "The Hidden World", "Echoes of Time", "The Final Empire", "Journey Through Shadows", "Silent Truth",
                    "Whispers of the Past", "The Last Kingdom", "Flames of Fate", "The Forgotten City", "Tears of Stone",
                    "Legacy of Ashes", "Winds of Destiny", "A Light in the Dark", "The Iron Throne", "City of Mist",
                    "Rise of the Fallen", "Shattered Dreams", "The Secret Garden", "Clockwork Heart", "Mirror's Edge",
                    "Path of the Warrior", "Beneath the Waves", "Veil of Night", "House of Cards", "Crown of Glass",
                    "The Winter Queen", "Bloodlines", "Broken Chains", "Realm of Fire", "Dawn of Tomorrow",
                    "The Dark Forest", "Chasing Starlight", "Maze of Memories", "Stormbound", "The Silent Blade",
                    "Call of the Wild", "Whirlwind", "Edge of Reality", "Golden Horizon", "Moonlit Veins"
                };

                var genres = new[]
                {
                    "Fantasy", "Science Fiction", "Romance", "Thriller", "Historical",
                    "Mystery", "Horror", "Non-Fiction", "Biography", "Adventure"
                };

                var random = new Random();
                var books = new List<Book>();

                for (int i = 0; i < 100; i++)
                {
                    var book = new Book
                    {
                        Title = titles[random.Next(titles.Length)],
                        Author = authors[random.Next(authors.Length)],
                        Genre = genres[random.Next(genres.Length)],
                        Price = Math.Round(100 + (decimal)random.NextDouble() * 400, 2)
                    };

                    books.Add(book);
                }

                context.Books.AddRange(books);
                context.SaveChanges();
            }

            // Tilføj testordrer, hvis der ikke er nogen
            if (!context.Orders.Any())
            {
                var sampleBooks = context.Books.Take(3).ToList(); 

                var orders = new List<Order>
                {
                    new Order { BookId = sampleBooks[0].Id, CustomerName = "Lasse" },
                    new Order { BookId = sampleBooks[1].Id, CustomerName = "Anna" },
                    new Order { BookId = sampleBooks[2].Id, CustomerName = "Mikkel" }
                };

                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
        }
    }
}
