using BookStore.BookStore.API.Models;
using BookStore.BookStore.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.BookStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IRepository<Book> _bookRepository;

    public BooksController(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _bookRepository.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _bookRepository.GetAsync(id);
        if (book == null)
            return NotFound();
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _bookRepository.AddAsync(book);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.Id)
            return BadRequest();

        try
        {
            await _bookRepository.EditAsync(book);
        }
        catch
        {
            if (await _bookRepository.GetAsync(id) == null)
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _bookRepository.GetAsync(id);
        if (book == null)
            return NotFound();

        await _bookRepository.RemoveAsync(id);
        return NoContent();
    }

    [HttpGet("genre-counts")]
    public async Task<ActionResult<Dictionary<string, int>>> GetBookCountsByGenre()
    {
        var knownGenres = new[]
        {
            "Fantasy", "Science Fiction", "Romance", "Thriller", "Historical",
            "Mystery", "Horror", "Non-Fiction", "Biography", "Adventure"
        };

        var books = await _bookRepository.GetAllAsync();
        var counts = knownGenres
            .Select(g => new { Genre = g, Count = books.Count(b => b.Genre == g) })
            .Where(g => g.Count > 0)
            .ToDictionary(g => g.Genre, g => g.Count);

        return Ok(counts);
    }

    [HttpGet("count-by-author")]
    public async Task<ActionResult<Dictionary<string, int>>> GetBookCountByAuthor()
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

        var books = await _bookRepository.GetAllAsync();
        var result = authors
            .Select(a => new { Author = a, Count = books.Count(b => b.Author == a) })
            .Where(a => a.Count > 0)
            .ToDictionary(a => a.Author, a => a.Count);

        return Ok(result);
    }

    [HttpGet("count-by-title")]
    public async Task<ActionResult<Dictionary<string, int>>> GetBookCountByTitle()
    {
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

        var books = await _bookRepository.GetAllAsync();
        var result = titles
            .Select(t => new { Title = t, Count = books.Count(b => b.Title == t) })
            .Where(t => t.Count > 0)
            .ToDictionary(t => t.Title, t => t.Count);

        return Ok(result);
    }

    [HttpGet("price-category/{price}")]
    public ActionResult<string> GetPriceCategory(decimal price)
    {
        if (price < 50)
            return Ok("Cheap");
        else if (price <= 150)
            return Ok("Moderate");
        else
            return Ok("Expensive");
    }
}
