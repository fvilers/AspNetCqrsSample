using System;
using System.Threading.Tasks;

namespace BookStore.Core
{
    public static class Extensions
    {
        public static Task StoreAsync<TAggregate>(this IAggregateRepository<TAggregate> repository, TAggregate aggregate, Guid? correlationId = null)
            where TAggregate : IAggregateRoot
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.StoreAsync(aggregate, correlationId?.ToString());
        }
    }
}
