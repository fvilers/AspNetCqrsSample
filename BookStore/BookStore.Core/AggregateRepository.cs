using BookStore.Core.EventSourcing;
using BookStore.Core.Messaging;
using BookStore.Core.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Core
{
    public class AggregateRepository<TAggregateRoot> : IAggregateRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        private static readonly string AggregateType = typeof(TAggregateRoot).Name;
        private readonly ITextSerializer _serializer;
        private readonly IEventStore _store;
        private readonly IEventBus _eventBus;

        public AggregateRepository(ITextSerializer serializer, IEventStore store, IEventBus eventBus)
        {
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            if (store == null) throw new ArgumentNullException(nameof(store));
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
            _serializer = serializer;
            _store = store;
            _eventBus = eventBus;
        }

        public async Task<TAggregateRoot> GetAsync(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty) throw new ArgumentNullException(nameof(aggregateId));

            var events = await _store.FindAsync(aggregateId).ConfigureAwait(false);
            var payloads = events
                .Select(x => DeserializeObject(x.Payload))
                .OrderBy(x => x.Version)
                .ToArray();

            if (!payloads.Any())
            {
                return null;
            }

            var parameters = new object[]
            {
                aggregateId,
                payloads
            };
            var constructor = typeof(TAggregateRoot).GetConstructor(new[] { typeof(Guid), typeof(IEnumerable<IVersionedEvent>) });

            if (constructor == null)
            {
                throw new InvalidOperationException("Missing ctor to load aggregate's history.");
            }

            var aggregate = (TAggregateRoot)constructor.Invoke(parameters);

            return aggregate;
        }

        public async Task StoreAsync(TAggregateRoot aggregate, string correlationId = null)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));

            var events = aggregate.UncommittedEvents.ToArray();
            var storedEvents = events.Select(@event => new StoredEvent
            {
                AggregateId = aggregate.Id,
                AggregateType = AggregateType,
                Version = @event.Version,
                Payload = SerializeObject(@event),
                CorrelationId = correlationId
            }).ToArray();
            var envelopes = events.Select(@event => new Envelope<IEvent>(@event)
            {
                CorrelationId = correlationId
            });

            await _store.SaveAsync(storedEvents).ConfigureAwait(false);
            await _eventBus.PublishAsync(envelopes).ConfigureAwait(false);
        }

        private string SerializeObject(IEvent e)
        {
            using (var writer = new StringWriter())
            {
                _serializer.Serialize(writer, e);

                return writer.ToString();
            }
        }

        private IVersionedEvent DeserializeObject(string payload)
        {
            using (var reader = new StringReader(payload))
            {
                return (IVersionedEvent)_serializer.Deserialize(reader);
            }
        }
    }
}