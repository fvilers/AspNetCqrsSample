using BookStore.Core.Messaging;
using System;

namespace BookStore.Domain.Commands
{
    public class RemoveBook : Command
    {
        public Guid BookId { get; set; }
    }
}
