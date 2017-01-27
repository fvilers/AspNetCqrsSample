using System;

namespace BookStore.Core.EventSourcing
{
    public class StoredEvent
    {
        public Guid AggregateId { get; set; }
        public string AggregateType { get; set; }
        public int Version { get; set; }
        public string Payload { get; set; }
        public string CorrelationId { get; set; }
    }
}
