using System;
using BookStore.BookStore.API.Data;
using BookStore.BookStore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BookStore.API.Controllers;
using Xunit;

namespace BookStore.Tests.Unit
{
    public class BooksControllerTests
    {
        private async Task<BookStoreContext> GetInMemoryDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<BookStoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            var context = new BookStoreContext(options);
            await context.Database.EnsureCreatedAsync();
            return context;
        }

        
        
        static int CountFantasyBooks(List<Book> books)
        {
            int count = 0;
            foreach (var book in books)
            {
                if (book.Genre == "Fantasy")
                    count++;
            }
            return count;
        }

        [Fact]
        public async Task GetBooks_ReturnsAllBooks()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            context.Books.AddRange(
                new Book { Title = "Book 1", Author = "Author 1", Genre = "Genre", Price = 10 },
                new Book { Title = "Book 2", Author = "Author 2", Genre = "Genre", Price = 15 }
            );
            await context.SaveChangesAsync();

            var controller = new BooksController(context);

            // Act
            var result = await controller.GetBooks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var books = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
            Assert.Equal(2, books.Count()); 
        }

        [Fact]
        public async Task GetBook_WithValidId_ReturnsCorrectBook()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            var book = new Book
            {
                Title = "Test Book",
                Author = "Test Author",
                Genre = "Test Genre",
                Price = 99.99m
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            var controller = new BooksController(context);

            // Act
            var result = await controller.GetBook(book.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBook = Assert.IsType<Book>(okResult.Value); 
            Assert.Equal(book.Title, returnedBook.Title);
            Assert.Equal(book.Author, returnedBook.Author);
        }


        [Fact]
        public async Task GetBook_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            var controller = new BooksController(context);

            // Act
            var result = await controller.GetBook(999); 

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public async Task PostBook_AddsBookToDatabase()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            var controller = new BooksController(context);

            var newBook = new Book
            {
                Title = "New Test Book",
                Author = "Test Author",
                Genre = "Fantasy",
                Price = 199.99m
            };

            // Act
            var result = await controller.PostBook(newBook);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdBook = Assert.IsType<Book>(createdAtActionResult.Value);

            Assert.Equal("New Test Book", createdBook.Title);
            Assert.Equal("Test Author", createdBook.Author);

            // Tjek at bogen faktisk findes i databasen nu
            var bookInDb = await context.Books.FindAsync(createdBook.Id);
            Assert.NotNull(bookInDb);
            Assert.Equal("New Test Book", bookInDb.Title);
        }
        
        [Fact]
        public async Task PutBook_UpdatesExistingBook()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();

            var originalBook = new Book
            {
                Title = "Original Title",
                Author = "Original Author",
                Genre = "Drama",
                Price = 150
            };
            context.Books.Add(originalBook);
            await context.SaveChangesAsync();
            
            context.Entry(originalBook).State = EntityState.Detached;

            var controller = new BooksController(context);

            var updatedBook = new Book
            {
                Id = originalBook.Id,
                Title = "Updated Title",
                Author = "Updated Author",
                Genre = "Science Fiction",
                Price = 299.95m
            };

            // Act
            var result = await controller.PutBook(originalBook.Id, updatedBook);

            // Assert
            Assert.IsType<NoContentResult>(result);

            var bookInDb = await context.Books.FindAsync(originalBook.Id);
            Assert.Equal("Updated Title", bookInDb!.Title);
            Assert.Equal("Updated Author", bookInDb.Author);
            Assert.Equal("Science Fiction", bookInDb.Genre);
            Assert.Equal(299.95m, bookInDb.Price);
        }
        
        [Fact]
        public async Task DeleteBook_RemovesBook_WhenBookExists()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();

            var book = new Book
            {
                Title = "To Be Deleted",
                Author = "Author X",
                Genre = "Thriller",
                Price = 99.99m
            };
            context.Books.Add(book);
            await context.SaveChangesAsync();

            var controller = new BooksController(context);

            // Act
            var result = await controller.DeleteBook(book.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Null(await context.Books.FindAsync(book.Id));
        }
        [Fact]
        public async Task DeleteBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            var controller = new BooksController(context);

            // Act
            var result = await controller.DeleteBook(999); 

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task PutBook_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            var controller = new BooksController(context);

            var book = new Book
            {
                Id = 1,
                Title = "Mismatch Test",
                Author = "Wrong ID Author",
                Genre = "Mismatch",
                Price = 29.99m
            };

            // Act
            var result = await controller.PutBook(2, book); 

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task PostBook_CreatesBook_ReturnsCreatedAtAction()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            var controller = new BooksController(context);

            var newBook = new Book
            {
                Title = "New Book",
                Author = "Test Author",
                Genre = "Education",
                Price = 19.99m
            };

            // Act
            var result = await controller.PostBook(newBook);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Book>(createdResult.Value);
            Assert.Equal("New Book", returnValue.Title);

            // Confirm it was actually saved
            var savedBook = await context.Books.FindAsync(returnValue.Id);
            Assert.NotNull(savedBook);
        }
        [Fact]
        public async Task PostBook_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            var controller = new BooksController(context);

            var invalidBook = new Book
            {
                // Title mangler – validering skal fejle
                Author = "Unknown",
                Genre = "Fiction",
                Price = 9.99m
            };
            
            controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await controller.PostBook(invalidBook);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value); 
        }

        [Fact]
        public async Task PostBook_ReturnsCreatedAtActionResult_WhenModelIsValid()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            var controller = new BooksController(context);

            var newBook = new Book
            {
                Title = "Test Book",
                Author = "Test Author",
                Genre = "Test Genre",
                Price = 19.99m
            };

            // Act
            var result = await controller.PostBook(newBook);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Book>(createdResult.Value);

            Assert.Equal("Test Book", returnValue.Title);
            Assert.Equal("Test Author", returnValue.Author);
            Assert.Equal("Test Genre", returnValue.Genre);
            Assert.Equal(19.99m, returnValue.Price);
        }
        
        
        [Fact]
        public async Task DeleteBook_RemovesBook()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            var controller = new BooksController(context);

            var book = new Book { Id = 1, Title = "Test Book", Author = "Author", Genre = "Genre", Price = 100 };
            context.Books.Add(book);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.DeleteBook(1);

            // Assert
            Assert.IsType<NoContentResult>(result);

            var deletedBook = await context.Books.FindAsync(1);
            Assert.Null(deletedBook); 
        }
        [Fact]
        public async Task DeleteBook_ReturnsNoContent_WhenBookIsDeleted()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            var controller = new BooksController(context);

            var book = new Book
            {
                Title = "To Be Deleted",
                Author = "Author X",
                Genre = "Drama",
                Price = 120.00m
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.DeleteBook(book.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        
        [Fact]
        public async Task GetBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            var controller = new BooksController(context);

            // Act
            var result = await controller.GetBook(999); 

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        
        [Fact]
        public async Task GetBookCountsByGenre_ReturnsCorrectCounts()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            context.Books.AddRange(
                new Book { Title = "Book A", Genre = "Fantasy", Author = "A", Price = 100 },
                new Book { Title = "Book B", Genre = "Science Fiction", Author = "B", Price = 120 },
                new Book { Title = "Book C", Genre = "Fantasy", Author = "C", Price = 110 },
                new Book { Title = "Book D", Genre = "Unknown Genre", Author = "D", Price = 90 }
            );
            await context.SaveChangesAsync();

            var controller = new BooksController(context);

            // Act
            var result = controller.GetBookCountsByGenre();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var counts = Assert.IsType<Dictionary<string, int>>(okResult.Value);

            Assert.Equal(2, counts["Fantasy"]);
            Assert.Equal(1, counts["Science Fiction"]);
            Assert.False(counts.ContainsKey("Unknown Genre")); // check at den ikke kom med
        }

        [Fact]
        public async Task GetBookCountByAuthor_ReturnsCorrectCounts()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            context.Books.AddRange(
                new Book { Title = "Book A", Author = "J.K. Rowling", Genre = "Fantasy", Price = 100 },
                new Book { Title = "Book B", Author = "George R.R. Martin", Genre = "Fantasy", Price = 120 },
                new Book { Title = "Book C", Author = "J.K. Rowling", Genre = "Fantasy", Price = 110 },
                new Book { Title = "Book D", Author = "Unknown Author", Genre = "Mystery", Price = 90 }
            );
            await context.SaveChangesAsync();

            var controller = new BooksController(context);

            // Act
            var result = controller.GetBookCountByAuthor();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var authorCounts = Assert.IsAssignableFrom<Dictionary<string, int>>(okResult.Value);

            Assert.Equal(2, authorCounts["J.K. Rowling"]);
            Assert.Equal(1, authorCounts["George R.R. Martin"]);
            Assert.False(authorCounts.ContainsKey("Unknown Author")); // false path
        }

        [Fact]
        public async Task GetBookCountByTitle_ReturnsCorrectCounts()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            context.Books.AddRange(
                new Book { Title = "The Final Empire", Author = "Brandon Sanderson", Genre = "Fantasy", Price = 100 },
                new Book { Title = "The Final Empire", Author = "Brandon Sanderson", Genre = "Fantasy", Price = 120 },
                new Book { Title = "Unknown Book", Author = "Mystery Author", Genre = "Mystery", Price = 90 }
            );
            await context.SaveChangesAsync();

            var controller = new BooksController(context);

            // Act
            var result = controller.GetBookCountByTitle();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var titleCounts = Assert.IsAssignableFrom<Dictionary<string, int>>(okResult.Value);

            Assert.Equal(2, titleCounts["The Final Empire"]);
            Assert.False(titleCounts.ContainsKey("Unknown Book")); // Ikke med i listen → ignoreres
        }

        [Theory]
        [InlineData(30, "Cheap")]
        [InlineData(75, "Moderate")]
        [InlineData(150, "Moderate")]
        [InlineData(200, "Expensive")]
        public async Task GetPriceCategory_ReturnsExpectedCategory(decimal inputPrice, string expectedCategory)
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync();
            var controller = new BooksController(context);

            // Act
            var result = controller.GetPriceCategory(inputPrice);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var category = Assert.IsType<string>(okResult.Value);
            Assert.Equal(expectedCategory, category);
        }

        public class BookUtilitiesTests
        {
            private readonly BooksController _controller;

            public BookUtilitiesTests()
            {
                _controller = new BooksController(null!); 
            }

            [Fact]
            public void CountFantasyBooks_ReturnsZero_WhenListIsEmpty()
            {
                // Arrange
                var books = new List<Book>();

                // Act
                var result = CountFantasyBooks(books);

                // Assert
                Assert.Equal(0, result);
            }

            [Fact]
            public void CountFantasyBooks_ReturnsZero_WhenNoFantasyBooks()
            {
                // Arrange
                var books = new List<Book>
                {
                    new Book { Title = "Book A", Genre = "Thriller" },
                    new Book { Title = "Book B", Genre = "Romance" }
                };

                // Act
                var result = CountFantasyBooks(books);

                // Assert
                Assert.Equal(0, result);
            }

            [Fact]
            public void CountFantasyBooks_ReturnsCorrectCount_WhenSomeAreFantasy()
            {
                // Arrange
                var books = new List<Book>
                {
                    new Book { Title = "Book A", Genre = "Fantasy" },
                    new Book { Title = "Book B", Genre = "Fantasy" },
                    new Book { Title = "Book C", Genre = "Thriller" }
                };

                // Act
                var result = CountFantasyBooks(books);

                // Assert
                Assert.Equal(2, result);
            }
        }

    }
}