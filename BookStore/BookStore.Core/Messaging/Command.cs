using System;

namespace BookStore.Core.Messaging
{
    public abstract class Command : ICommand
    {
        public Guid Id { get; }

        protected Command()
        {
            Id = Guid.NewGuid();
        }
    }
}