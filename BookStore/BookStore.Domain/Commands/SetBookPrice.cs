using BookStore.Core.Messaging;
using System;

namespace BookStore.Domain.Commands
{
    public class SetBookPrice : Command
    {
        public Guid BookId { get; set; }
        public decimal Price { get; set; }
    }
}
