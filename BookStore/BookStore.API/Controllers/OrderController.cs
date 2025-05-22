using Microsoft.AspNetCore.Mvc;
using BookStore.BookStore.API.Models;
using BookStore.BookStore.API.Interfaces;

namespace BookStore.BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<Book> _bookRepo;

        public OrderController(IRepository<Order> orderRepo, IRepository<Book> bookRepo)
        {
            _orderRepo = orderRepo;
            _bookRepo = bookRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderRepo.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderRepo.GetAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            var books = await _bookRepo.GetAllAsync();
            bool bookExists = books.Any(b => b.Id == order.BookId);
            if (!bookExists)
                return NotFound($"Book with ID {order.BookId} not found.");

            var orders = await _orderRepo.GetAllAsync();
            bool duplicateOrder = orders.Any(o => o.BookId == order.BookId && o.CustomerName == order.CustomerName);
            if (duplicateOrder)
                return Conflict("An order with the same book and customer already exists.");

            await _orderRepo.AddAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepo.GetAsync(id);
            if (order == null) return NotFound();

            await _orderRepo.RemoveAsync(id);
            return NoContent();
        }
    }
}
