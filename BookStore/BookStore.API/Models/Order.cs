using System.ComponentModel.DataAnnotations;

namespace BookStore.BookStore.API.Models;

public class Order
{
    public int Id { get; set; }

    [Required(ErrorMessage = "BookId is required")]
    public int BookId { get; set; }

    [Required(ErrorMessage = "CustomerName is required")]
    [MinLength(1, ErrorMessage = "CustomerName must not be empty")]
    [MaxLength(100, ErrorMessage = "CustomerName must be 100 characters or fewer")]
    public string CustomerName { get; set; } = string.Empty;

    public Book? Book { get; set; }
}