using System;

namespace BookStore.Core.Messaging
{
    public interface IEvent : IMessage
    {
        Guid SourceId { get; set; }
    }
}