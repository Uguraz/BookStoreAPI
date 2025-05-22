using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Reqnroll;
using Assert = Xunit.Assert;
using BookStore.BookStore.API.Models;
using BookStore.BookStore.API.Data;

namespace Bookstore.Test.Cucumber.MyReqnrollProject.StepDefinitions;

[Binding]
public sealed class OrderStepDefinitions
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private HttpResponseMessage _response;
    private int _bookId;
    private string _customerName = "";
    private bool _bookIdIsValid = true;

    public OrderStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [BeforeScenario]
    public async Task ClearOrdersBeforeEachScenario()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookStoreContext>(); // <-- Ændret her

        dbContext.Orders.RemoveRange(dbContext.Orders);
        await dbContext.SaveChangesAsync();
    }


    [Given("the book ID is {int}")]
    public void GivenTheBookIdIs(int id)
    {
        _bookId = id;
        _bookIdIsValid = true;
    }

    [Given("the book ID is {string}")]
    public void GivenTheBookIdIsString(string id)
    {
        if (int.TryParse(id, out var parsedId))
        {
            _bookId = parsedId;
            _bookIdIsValid = true;
        }
        else
        {
            _bookIdIsValid = false;
        }
    }

    [Given("the customer name is {string}")]
    public void GivenTheCustomerNameIs(string name)
    {
        _customerName = name;
    }

    [Given("the customer name is \"(.*)\" repeated (\\d+) times")]
    public void GivenTheCustomerNameIsRepeated(string name, int times)
    {
        _customerName = string.Concat(Enumerable.Repeat(name, times));
    }

    [Given("the order has already been submitted")]
    public async Task GivenTheOrderHasAlreadyBeenSubmitted()
    {
        if (!_bookIdIsValid) return;

        var order = new Order
        {
            BookId = _bookId,
            CustomerName = _customerName
        };

        await _client.PostAsJsonAsync("/api/Order", order);
    }

    [When("the order is submitted")]
    public async Task WhenTheOrderIsSubmitted()
    {
        if (!_bookIdIsValid)
        {
            // Send rå JSON med ugyldigt bookId
            var orderJson = new
            {
                BookId = "INVALID",
                CustomerName = _customerName
            };

            _response = await _client.PostAsJsonAsync("/api/Order", orderJson);
            return;
        }

        var order = new Order
        {
            BookId = _bookId,
            CustomerName = _customerName
        };

        _response = await _client.PostAsJsonAsync("/api/Order", order);
    }

    [Then("the response status should be {int}")]
    public void ThenTheResponseStatusShouldBe(int statusCode)
    {
        Assert.Equal((HttpStatusCode)statusCode, _response.StatusCode);
    }

    [Then("the response should contain error message {string}")]
    public async Task ThenTheResponseShouldContainErrorMessage(string expectedMessage)
    {
        var content = await _response.Content.ReadAsStringAsync();
        Assert.Contains(expectedMessage, content);
    }
}
