using System;

namespace BookStore.Core.Messaging
{
    public interface ICommand : IMessage
    {
        Guid Id { get; }
    }
}