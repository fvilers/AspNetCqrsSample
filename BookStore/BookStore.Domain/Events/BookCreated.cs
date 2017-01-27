using BookStore.Core.EventSourcing;

namespace BookStore.Domain.Events
{
    public class BookCreated : VersionedEvent
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
