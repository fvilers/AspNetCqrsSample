using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Core.ReadModels
{
    public interface IDao<TReadModel>
        where TReadModel : IReadModel
    {
        Task<IEnumerable<TReadModel>> FindAsync();
        Task<TReadModel> GetAsync(Guid aggregateId);
        void Add(Guid aggregateId, TReadModel value);
        void Remove(Guid aggregateId);
    }
}