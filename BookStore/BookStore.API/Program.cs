using BookStore.BookStore.API.Data;
using BookStore.BookStore.API.Interfaces;
using Microsoft.EntityFrameworkCore;





var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseSqlite("Data Source=bookstore.db"));
builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Seed database (altid)
DbInitializer.Seed(app);

// Configure middleware
app.UseRouting();
app.MapControllers();

app.Run();

public partial class Program { }