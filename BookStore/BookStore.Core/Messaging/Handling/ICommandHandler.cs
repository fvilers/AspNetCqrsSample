using System.Threading.Tasks;

namespace BookStore.Core.Messaging.Handling
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in T> : ICommandHandler
        where T : ICommand
    {
        Task HandleAsync(T command);
    }
}