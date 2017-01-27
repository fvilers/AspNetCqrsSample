using BookStore.Core.EventSourcing;
using BookStore.Core.Messaging;
using System;
using System.Collections.Generic;

namespace BookStore.Core
{
    public class AggregateRoot : Entity, IAggregateRoot, IEventSourced
    {
        public int Version { get; private set; }
        public IEnumerable<IVersionedEvent> UncommittedEvents => _uncommittedEvents;

        private readonly List<IVersionedEvent> _uncommittedEvents = new List<IVersionedEvent>();
        private readonly Dictionary<Type, Action<IVersionedEvent>> _handlers = new Dictionary<Type, Action<IVersionedEvent>>();

        protected AggregateRoot(Guid id)
            : base(id)
        {
        }

        protected void Handle<T>(Action<T> handler)
            where T : IEvent
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            _handlers.Add(typeof(T), @event => handler((T)@event));
        }

        protected void Raise(IVersionedEvent @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            @event.SourceId = Id;
            @event.Version = Version + 1;

            Raise(@event, true);
        }

        protected void Rehydrate(IEnumerable<IVersionedEvent> history)
        {
            if (history == null) throw new ArgumentNullException(nameof(history));

            foreach (var @event in history)
            {
                Raise(@event, false);
            }
        }

        private void Raise(IVersionedEvent @event, bool isNew)
        {
            Action<IVersionedEvent> handler;
            if (_handlers.TryGetValue(@event.GetType(), out handler))
            {
                handler(@event);
            }

            Version = @event.Version;

            if (isNew)
            {
                _uncommittedEvents.Add(@event);
            }
        }
    }
}