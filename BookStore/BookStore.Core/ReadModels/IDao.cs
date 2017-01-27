using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Core.ReadModels
{
    public interface IDao<TReadModel>
    {
        Task<IEnumerable<TReadModel>> FindAsync();
        Task<TReadModel> GetAsync(Guid aggregateId);
        void Add(Guid aggregateId, TReadModel value);
    }
}