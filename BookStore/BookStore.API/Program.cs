using BookStore.BookStore.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseSqlite("Data Source=bookstore.db"));
builder.Services.AddControllers();

var app = builder.Build();

// Seed database (altid)
DbInitializer.Seed(app);

// Configure middleware
app.UseRouting();
app.MapControllers();

app.Run();

public partial class Program { }