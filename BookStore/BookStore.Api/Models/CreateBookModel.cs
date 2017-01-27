using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Models
{
    public class CreateBookModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}