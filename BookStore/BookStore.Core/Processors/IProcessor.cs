using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Core.Processors
{
    public interface IProcessor
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}