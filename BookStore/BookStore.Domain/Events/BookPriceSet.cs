using BookStore.Core.EventSourcing;

namespace BookStore.Domain.Events
{
    public class BookPriceSet : VersionedEvent
    {
        public decimal Price { get; set; }
    }
}