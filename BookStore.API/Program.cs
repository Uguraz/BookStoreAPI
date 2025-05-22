using Microsoft.EntityFrameworkCore;
using BookStore.BookStore.API.Models;



var builder = WebApplication.CreateBuilder(args);

// Tilføj din DbContext
builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseSqlite("Data Source=bookstore.db"));

builder.Services.AddControllers();
var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.Run();
