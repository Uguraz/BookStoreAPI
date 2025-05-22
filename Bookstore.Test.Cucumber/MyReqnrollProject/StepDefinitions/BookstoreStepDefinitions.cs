using System.Net.Http.Json;
using BookStore.BookStore.API.Data;
using BookStore.BookStore.API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;


namespace Bookstore.Test.Cucumber.MyReqnrollProject.StepDefinitions;

[Binding]
public class OrderStepDefinitions
{
    private HttpClient _client;
    private HttpResponseMessage _response;
    private WebApplicationFactory<Program> _factory;
    private string _customerName;

    [BeforeScenario]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BookStoreContext>();
        db.Database.EnsureCreated();

        db.Orders.RemoveRange(db.Orders);
        db.Books.RemoveRange(db.Books);
        db.SaveChanges();

        db.Books.Add(new Book
        {
            Id = 101,
            Title = "Test Book",
            Author = "Test Author",
            Genre = "Fantasy",
            Price = 100
        });

        db.SaveChanges();
    }

    [AfterScenario]
    public void TearDown()
    {
        _factory?.Dispose();
        _client?.Dispose();
    }

    // Scenario: Creating a new order
    [Given("a valid book with ID 101 exists")]
    public void GivenValidBookExists()
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BookStoreContext>();
    
        var book = db.Books.Find(101);
        Xunit.Assert.NotNull(book);
    }

    [Given("the customer name is Lasse")]
    public void GivenCustomerNameIsLasse()
    {
        _customerName = "Lasse";
    }

    [When("the order is submitted with book ID 101 and customer name Lasse")]
    public async Task WhenOrderIsSubmittedWithValidData()
    {
        var order = new Order { BookId = 101, CustomerName = "Lasse" };
        _response = await _client.PostAsJsonAsync("/api/order", order);
    }

    [Then("the response status should be 201")]
    public void ThenStatusShouldBe201()
    {
        Xunit.Assert.Equal(201, (int)_response.StatusCode);
    }

    // Scenario: Creating an order with an invalid book ID
    [Given("the customer name is Lasse for an invalid book ID")]
    public void GivenCustomerNameForInvalidBook()
    {
        _customerName = "Lasse";
    }

    [When("the order is submitted with book ID -1 and customer name Lasse")]
    public async Task WhenOrderSubmittedInvalidBookId()
    {
        var order = new Order { BookId = -1, CustomerName = "Lasse" };
        _response = await _client.PostAsJsonAsync("/api/order", order);
    }

    [Then("the response status should be 404")]
    public void ThenStatusShouldBe404()
    {
        Xunit.Assert.Equal(404, (int)_response.StatusCode);
    }

    // Scenario: Creating an order with a missing customer name
    
    // Given a valid book with ID 101 exists reused from Creating a new order
    
    [When("the order is submitted with book ID 101 and empty customer name")]
    public async Task WhenOrderSubmittedWithEmptyName()
    {
        var order = new Order { BookId = 101, CustomerName = "" };
        _response = await _client.PostAsJsonAsync("/api/order", order);
    }

    [Then("the response status should be 400")]
    public void ThenStatusShouldBe400()
    {
        Xunit.Assert.Equal(400, (int)_response.StatusCode);
    }

    // Scenario: Creating an order with a duplicate request
    [Given("the customer name is Lasse for duplicate request")]
    public void GivenCustomerNameForDuplicate()
    {
        _customerName = "Lasse";
    }

    [Given("the order has already been submitted")]
    public async Task GivenOrderAlreadySubmitted()
    {
        var order = new Order { BookId = 101, CustomerName = "Lasse" };
        await _client.PostAsJsonAsync("/api/order", order);
    }

    [When("the duplicate order is submitted")]
    public async Task WhenDuplicateOrderSubmitted()
    {
        var order = new Order { BookId = 101, CustomerName = "Lasse" };
        _response = await _client.PostAsJsonAsync("/api/order", order);
    }

    [Then("the response status should be 409")]
    public void ThenStatusShouldBe409()
    {
        Xunit.Assert.Equal(409, (int)_response.StatusCode);
    }

    // Scenario: Creating an order with a very long customer name
    [Given("the customer name is Lasse repeated 50 times")]
    public void GivenCustomerNameIsLong()
    {
        _customerName = string.Concat(Enumerable.Repeat("Lasse", 50));
    }

    [When("the order is submitted")]
    public async Task WhenOrderSubmittedWithLongName()
    {
       
        var order = new Order { BookId = 101, CustomerName = _customerName };
        _response = await _client.PostAsJsonAsync("/api/order", order);
    }
    
    // Then Response status 400 reused from Creating an order with a duplicate request

 
}
