using BookStore.Core.Messaging;

namespace BookStore.Domain.Commands
{
    public class CreateBook : Command
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }
}
