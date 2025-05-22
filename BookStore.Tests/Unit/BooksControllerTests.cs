using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BookStore.API.Controllers;
using BookStore.BookStore.API.Interfaces;
using BookStore.BookStore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookStore.Tests.Unit
{
    public class BooksControllerTests
    {
        private readonly BooksController _controller;
        private readonly Mock<IRepository<Book>> _mockBookRepo;
        private readonly List<Book> _bookData;

        public BooksControllerTests()
        {
            _bookData = new List<Book>
            {
                new Book { Id = 1, Title = "Book One", Author = "Author One", Genre = "Fiction", Price = 16 },
                new Book { Id = 2, Title = "Book Two", Author = "Author Two", Genre = "Non-Fiction", Price = 20 },
                new Book { Id = 3, Title = "Book Three", Author = "Author Three", Genre = "Non-Fiction", Price = 22 },
                new Book { Id = 4, Title = "Book Four", Author = "Author Four", Genre = "Non-Fiction", Price = 25 },
                new Book { Id = 5, Title = "Book Five", Author = "Author Five", Genre = "Non-Fiction", Price = 29 },
                new Book { Id = 6, Title = "Book Six", Author = "Author Six", Genre = "Non-Fiction", Price = 31 },
                new Book { Id = 7, Title = "Book Seven", Author = "Author Seven", Genre = "Non-Fiction", Price = 37 },
                new Book { Id = 8, Title = "Book Eight", Author = "Author Eight", Genre = "Non-Fiction", Price = 50 }
            };

            _mockBookRepo = new Mock<IRepository<Book>>();

            _mockBookRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_bookData);
            _mockBookRepo.Setup(repo => repo.GetAsync(It.IsInRange(1, 2, Moq.Range.Inclusive)))
                         .ReturnsAsync((int id) => _bookData.FirstOrDefault(b => b.Id == id));
            _mockBookRepo.Setup(repo => repo.GetAsync(It.Is<int>(id => id < 1 || id > 2)))
                         .ReturnsAsync((Book)null);
            _mockBookRepo.Setup(repo => repo.AddAsync(It.IsAny<Book>()))
                         .Callback<Book>(b => _bookData.Add(b))
                         .Returns(Task.CompletedTask);

            _controller = new BooksController(_mockBookRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsCorrectBooks()
        {
            // Act
            var result = await _controller.GetBooks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var books = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
            Assert.Equal(8, books.Count());
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsBook()
        {
            // Act
            var result = await _controller.GetBook(2);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var book = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(2, book.Id);
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.GetBook(-1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostBook_ValidBook_ReturnsCreated()
        {
            // Arrange
            var newBook = new Book { Id = 3, Title = "New Book", Author = "Author", Genre = "Mystery", Price = 25 };

            // Act
            var result = await _controller.PostBook(newBook);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdBook = Assert.IsType<Book>(createdResult.Value);
            Assert.Equal("New Book", createdBook.Title);
            Assert.Contains(_bookData, b => b.Id == 3);
        }

        [Fact]
        public async Task FilterBooksByPriceRange()
        {
            // Arrange
            decimal minPrice = 15;
            decimal maxPrice = 30;

            // Act
            var result = await _controller.GetBooks();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var books = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);

            var filtered = new List<Book>();
            foreach (var book in books)
            {
                if (book.Price >= minPrice && book.Price <= maxPrice)
                {
                    filtered.Add(book);
                }
            }

            // Assert
            Assert.Equal(5, filtered.Count);
            Assert.All(filtered, b => Assert.InRange(b.Price, minPrice, maxPrice));
        }
    }
}
