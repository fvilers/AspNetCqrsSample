using System;

namespace BookStore.Core.Messaging
{
    public abstract class Event : IEvent
    {
        public Guid SourceId { get; set; }
    }
}