using BookStore.Core.Messaging;
using System;

namespace BookStore.Domain.Commands
{
    public class UpdateBook : Command
    {
        public Guid BookId { get; set; }
        public decimal Price { get; set; }
    }
}
