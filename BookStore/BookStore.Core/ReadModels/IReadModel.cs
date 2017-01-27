using System;

namespace BookStore.Core.ReadModels
{
    public interface IReadModel
    {
        Guid AggregateId { get; set; }
    }
}