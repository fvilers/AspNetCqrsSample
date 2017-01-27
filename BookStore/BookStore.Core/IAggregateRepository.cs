using System;
using System.Threading.Tasks;

namespace BookStore.Core
{
    public interface IAggregateRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        Task<TAggregateRoot> GetAsync(Guid aggregateId);
        Task StoreAsync(TAggregateRoot aggregate, string correlationId = null);
    }
}