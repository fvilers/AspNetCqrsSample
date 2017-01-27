using System;
using System.Threading.Tasks;

namespace BookStore.Core.Messaging
{
    public static class Extensions
    {
        public static Task SendAsync(this ICommandBus commandBus, ICommand command)
        {
            if (commandBus == null) throw new ArgumentNullException(nameof(commandBus));
            if (command == null) throw new ArgumentNullException(nameof(command));

            return commandBus.SendAsync(new Envelope<ICommand>(command));
        }
    }
}
