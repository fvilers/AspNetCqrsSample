using BookStore.Core.Messaging;
using BookStore.Core.Messaging.Handling;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Processors
{
    public class EventProcessor : MessageProcessor<IEvent>
    {
        private readonly IEventDispatcher _dispatcher;

        public EventProcessor(IEventDispatcher dispatcher, IProducerConsumerCollection<Envelope<IEvent>> commandCollection)
            : base(commandCollection)
        {
            if (dispatcher == null) throw new ArgumentNullException(nameof(dispatcher));
            _dispatcher = dispatcher;
        }

        protected override async Task ProcessMessageAsync(IEvent message, string correlationId)
        {
            await _dispatcher.DispatchEventAsync(message, correlationId).ConfigureAwait(false);
        }
    }
}