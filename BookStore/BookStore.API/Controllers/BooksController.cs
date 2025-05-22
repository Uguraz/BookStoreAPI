using BookStore.BookStore.API.Data;
using BookStore.BookStore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookStoreContext _context;

    public BooksController(BookStoreContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _context.Books.ToListAsync();
        return Ok(books); 
    }


    // GET: api/books/
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        return Ok(book); 
    }
    
    // POST: api/books/
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }


    // PUT: api/books/
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.Id)
            return BadRequest();

        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await BookExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/books/
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> BookExists(int id)
    {
        return await _context.Books.AnyAsync(e => e.Id == id);
    }
    
    // Returnerer antal bøger pr. genre (kun for kendte genrer)
    [HttpGet("genre-counts")]
    public ActionResult<Dictionary<string, int>> GetBookCountsByGenre()
    {
        var knownGenres = new[]
        {
            "Fantasy", "Science Fiction", "Romance", "Thriller", "Historical",
            "Mystery", "Horror", "Non-Fiction", "Biography", "Adventure"
        };

        var counts = new Dictionary<string, int>();

        foreach (var genre in knownGenres)
        {
            int count = _context.Books.Count(b => b.Genre == genre);
            if (count > 0)
            {
                counts[genre] = count;
            }
        }

        return Ok(counts);
    }
    
    [HttpGet("count-by-author")]
    public ActionResult<Dictionary<string, int>> GetBookCountByAuthor()
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

        var result = new Dictionary<string, int>();

        foreach (var author in authors)
        {
            int count = _context.Books.Count(b => b.Author == author);
            if (count > 0)
            {
                result[author] = count;
            }
        }

        return Ok(result);
    }

    [HttpGet("count-by-title")]
    public ActionResult<Dictionary<string, int>> GetBookCountByTitle()
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

        var result = new Dictionary<string, int>();

        foreach (var title in titles)
        {
            int count = _context.Books.Count(b => b.Title == title);
            if (count > 0)
            {
                result[title] = count;
            }
        }

        return Ok(result);
    }

    [HttpGet("price-category/{price}")]
    public ActionResult<string> GetPriceCategory(decimal price)
    {
        if (price < 50)
            return Ok("Cheap");
        else if (price >= 50 && price <= 150)
            return Ok("Moderate");
        else
            return Ok("Expensive");
    }




}