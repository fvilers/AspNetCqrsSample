using BookStore.Core.Messaging;
using System;

namespace BookStore.Domain.Commands
{
    public class DeleteBook : Command
    {
        public Guid BookId { get; set; }
    }
}
