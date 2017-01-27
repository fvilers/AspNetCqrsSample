using BookStore.Core.Messaging;
using BookStore.Core.Messaging.Handling;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Processors
{
    public class CommandProcessor : MessageProcessor<ICommand>
    {
        private readonly ICommandExecuter _executer;

        public CommandProcessor(ICommandExecuter executer, IProducerConsumerCollection<Envelope<ICommand>> commandCollection)
            : base(commandCollection)
        {
            if (executer == null) throw new ArgumentNullException(nameof(executer));
            _executer = executer;
        }

        protected override async Task ProcessMessageAsync(ICommand message, string correlationId)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            await _executer.ExecuteAsync(message, correlationId).ConfigureAwait(false);
        }
    }
}