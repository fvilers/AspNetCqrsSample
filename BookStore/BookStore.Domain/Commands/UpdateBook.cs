using BookStore.Core.Messaging;

namespace BookStore.Domain.Commands
{
    public class UpdateBook : Command
    {
        public decimal Price { get; set; }
    }
}
