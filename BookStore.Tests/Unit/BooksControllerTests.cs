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
                new Book { Id = 1, Title = "Book One", Author = "Author One", Genre = "Fiction", Price = 20 },
                new Book { Id = 2, Title = "Book Two", Author = "Author Two", Genre = "Non-Fiction", Price = 25 }
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
            var result = await _controller.GetBooks();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var books = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
            Assert.Equal(2, books.Count());
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsBook()
        {
            var result = await _controller.GetBook(2);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var book = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(2, book.Id);
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsNotFound()
        {
            var result = await _controller.GetBook(999);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostBook_ValidBook_ReturnsCreated()
        {
            var newBook = new Book { Id = 3, Title = "New Book", Author = "Author", Genre = "Mystery", Price = 25 };

            var result = await _controller.PostBook(newBook);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdBook = Assert.IsType<Book>(createdResult.Value);
            Assert.Equal("New Book", createdBook.Title);
            Assert.Contains(_bookData, b => b.Id == 3);
        }
    }
}
