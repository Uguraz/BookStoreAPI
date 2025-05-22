using Microsoft.AspNetCore.Mvc;
using BookStore.BookStore.API.Data;
using BookStore.BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public OrderController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            return order;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            // Tjek om bogen findes
            var bookExists = await _context.Books.AnyAsync(b => b.Id == order.BookId);
            if (!bookExists)
                return NotFound($"Book with ID {order.BookId} not found.");

            // Tjek for duplikeret ordre
            var duplicateOrder = await _context.Orders
                .AnyAsync(o => o.BookId == order.BookId && o.CustomerName == order.CustomerName);

            if (duplicateOrder)
                return Conflict("An order with the same book and customer already exists.");

            // Opret ny ordre
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}