namespace BookStore.BookStore.API.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }

}