using System.Collections.Generic;

namespace BookStore.Core.EventSourcing
{
    public interface IEventSourced
    {
        int Version { get; }
        IEnumerable<IVersionedEvent> UncommittedEvents { get; }
    }
}