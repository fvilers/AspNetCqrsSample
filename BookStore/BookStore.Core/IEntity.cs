using System;

namespace BookStore.Core
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}