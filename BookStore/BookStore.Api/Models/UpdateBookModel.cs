using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Models
{
    public class UpdateBookModel
    {
        [Required]
        public decimal Price { get; set; }
    }
}