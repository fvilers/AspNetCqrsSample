using BookStore.Core.Messaging;

namespace BookStore.Core.EventSourcing
{
    public interface IVersionedEvent : IEvent
    {
        int Version { get; set; }
    }
}