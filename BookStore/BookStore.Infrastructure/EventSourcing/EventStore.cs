using BookStore.Core.Collections.Generic;
using BookStore.Core.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.EventSourcing
{
    public class EventStore : IEventStore
    {
        private readonly ICollection<StoredEvent> _store = new HashSet<StoredEvent>();

        public Task<IEnumerable<StoredEvent>> FindAsync(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty) throw new ArgumentNullException(nameof(aggregateId));

            var query = from @event in _store
                        where @event.AggregateId == aggregateId
                        select @event;
            var result = query.ToArray();

            return Task.FromResult(result.AsEnumerable());
        }

        public Task SaveAsync(IEnumerable<StoredEvent> events)
        {
            if (events == null) throw new ArgumentNullException(nameof(events));

            return Task.Run(() => _store.AddRange(events));
        }
    }
}
