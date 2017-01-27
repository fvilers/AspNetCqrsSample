using System.Threading.Tasks;

namespace BookStore.Core.Messaging.Handling
{
    public interface ICommandExecuter
    {
        Task ExecuteAsync(ICommand command, string correlationId = null);
    }
}