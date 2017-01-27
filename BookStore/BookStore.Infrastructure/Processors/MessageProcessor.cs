using BookStore.Core.Messaging;
using BookStore.Core.Processors;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Processors
{
    public abstract class MessageProcessor<T> : IProcessor
        where T : IMessage
    {
        private readonly IProducerConsumerCollection<Envelope<T>> _commandCollection;

        protected MessageProcessor(IProducerConsumerCollection<Envelope<T>> commandCollection)
        {
            if (commandCollection == null) throw new ArgumentNullException(nameof(commandCollection));
            _commandCollection = commandCollection;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(async () => await StartPollingAsync(cancellationToken).ConfigureAwait(false), cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }

        protected abstract Task ProcessMessageAsync(T message, string correlationId);

        private async Task StartPollingAsync(CancellationToken cancellationToken)
        {
            // TODO: make the poll delay configurable
            var pollDelay = TimeSpan.FromMilliseconds(250);

            while (!cancellationToken.IsCancellationRequested)
            {
                Envelope<T> envelope;
                if (_commandCollection.TryTake(out envelope))
                {
                    try
                    {
                        await ProcessMessageAsync(envelope.Body, envelope.CorrelationId).ConfigureAwait(false);
                    }
                    catch (Exception)
                    {
                        Debugger.Break();
                    }
                }

                await Task.Delay(pollDelay, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
