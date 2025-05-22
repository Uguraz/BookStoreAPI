using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BookStore.BookStore.API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace BookStore.Tests.Integration;

public class BookApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BookApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetBooks_ReturnsListOfBooks()
    {
        // Act
        var response = await _client.GetAsync("/api/books");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var books = await response.Content.ReadFromJsonAsync<List<Book>>();
        Assert.NotNull(books);
        Assert.True(books.Count > 0); // Du har seeded med 100 bøger
    }
    
    [Fact]
    public async Task GetBooks_ReturnsSuccessAndAllBooks()
    {
        // Act
        var response = await _client.GetAsync("/api/books");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var books = await response.Content.ReadFromJsonAsync<List<Book>>();
        Assert.NotNull(books);
        Assert.True(books.Count >= 1); // Du burde have seedet 100 bøger
    }
    
    [Fact]
    public async Task GetBook_ReturnsCorrectBook()
    {
        // Arrange – opret en bog først
        var newBook = new Book
        {
            Title = "Testbog",
            Author = "Testforfatter",
            Genre = "Testgenre",
            Price = 199.99m
        };

        var postResponse = await _client.PostAsJsonAsync("/api/books", newBook);
        postResponse.EnsureSuccessStatusCode();

        var createdBook = await postResponse.Content.ReadFromJsonAsync<Book>();

        // Act – hent bogen med det ID vi fik tilbage
        var getResponse = await _client.GetAsync($"/api/books/{createdBook!.Id}");

        // Assert
        getResponse.EnsureSuccessStatusCode();
        var fetchedBook = await getResponse.Content.ReadFromJsonAsync<Book>();

        Assert.NotNull(fetchedBook);
        Assert.Equal(createdBook.Id, fetchedBook!.Id);
        Assert.Equal("Testbog", fetchedBook.Title);
    }
    
    [Fact]
    public async Task GetBook_ReturnsNotFound_ForInvalidId()
    {
        // Arrange
        var invalidId = -999;

        // Act
        var response = await _client.GetAsync($"/api/books/{invalidId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PostBook_ReturnsCreated_WhenValid()
    {
        // Arrange
        var newBook = new
        {
            Title = "Test Title",
            Author = "Test Author",
            Genre = "Test Genre",
            Price = 199.99M
        };

        var content = new StringContent(JsonConvert.SerializeObject(newBook), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/books", content);

        // Assert
        response.EnsureSuccessStatusCode(); // status 2xx
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var returnedBook = JsonConvert.DeserializeObject<Book>(responseString);

        Assert.Equal(newBook.Title, returnedBook!.Title);
        Assert.Equal(newBook.Author, returnedBook.Author);
        Assert.Equal(newBook.Genre, returnedBook.Genre);
    }

    [Fact]
    public async Task PostBook_ReturnsBadRequest_WhenInvalid()
    {
        // Arrange – mangler Title
        var invalidBook = new
        {
            Author = "Test Author",
            Genre = "Test Genre",
            Price = 149.99M
        };

        var content = new StringContent(JsonConvert.SerializeObject(invalidBook), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/books", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    [Fact]
    public async Task PutBook_UpdatesBook_WhenValid()
    {
        // Arrange – opret en ny bog først
        var originalBook = new
        {
            Title = "Original Title",
            Author = "Original Author",
            Genre = "Original Genre",
            Price = 99.99M
        };

        var postResponse = await _client.PostAsync("/api/books", 
            new StringContent(JsonConvert.SerializeObject(originalBook), Encoding.UTF8, "application/json"));

        postResponse.EnsureSuccessStatusCode();

        var createdContent = await postResponse.Content.ReadAsStringAsync();
        var createdBook = JsonConvert.DeserializeObject<Book>(createdContent);

        // Act – opdater bogen
        var updatedBook = new
        {
            Id = createdBook.Id,
            Title = "Updated Title",
            Author = "Updated Author",
            Genre = "Updated Genre",
            Price = 199.99M
        };

        var putResponse = await _client.PutAsync($"/api/books/{createdBook.Id}",
            new StringContent(JsonConvert.SerializeObject(updatedBook), Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);
    }
    
    [Fact]
    public async Task DeleteBook_RemovesBook_WhenExists()
    {
        // Arrange – opret en ny bog først
        var bookToDelete = new
        {
            Title = "Temp Title",
            Author = "Temp Author",
            Genre = "Temp Genre",
            Price = 149.99M
        };

        var postResponse = await _client.PostAsync("/api/books",
            new StringContent(JsonConvert.SerializeObject(bookToDelete), Encoding.UTF8, "application/json"));

        postResponse.EnsureSuccessStatusCode();

        var content = await postResponse.Content.ReadAsStringAsync();
        var createdBook = JsonConvert.DeserializeObject<Book>(content);

        // Act – slet bogen
        var deleteResponse = await _client.DeleteAsync($"/api/books/{createdBook.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Prøv at hente bogen igen og forvent 404
        var getResponse = await _client.GetAsync($"/api/books/{createdBook.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    
}