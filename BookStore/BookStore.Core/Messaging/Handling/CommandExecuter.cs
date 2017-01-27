using System;
using System.Threading.Tasks;

namespace BookStore.Core.Messaging.Handling
{
    public class CommandExecuter : ICommandExecuter
    {
        private readonly ICommandHandlerRegistry _registry;

        public CommandExecuter(ICommandHandlerRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException(nameof(registry));
            _registry = registry;
        }

        public async Task ExecuteAsync(ICommand command, string correlationId = null)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var commandType = command.GetType();
            ICommandHandler handler;

            if (_registry.TryGetHandler(commandType, out handler))
            {
                await ((dynamic)handler).HandleAsync((dynamic)command).ConfigureAwait(false);
            }
        }
    }
}
