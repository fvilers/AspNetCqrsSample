using BookStore.Core.Messaging;

namespace BookStore.Core.EventSourcing
{
    public class VersionedEvent : Event, IVersionedEvent
    {
        public int Version { get; set; }
    }
}
