using BookStore.Core.Messaging.Handling;
using BookStore.Core.ReadModels;
using BookStore.Domain.Events;
using System;
using System.Threading.Tasks;

namespace BookStore.Api.ReadModels.EventHandlers
{
    public class BookDenormalizer : IEventHandler<BookCreated>, IEventHandler<BookPriceSet>, IEventHandler<BookRemoved>
    {
        private readonly IDao<BookReadModel> _dao;

        public BookDenormalizer(IDao<BookReadModel> dao)
        {
            if (dao == null) throw new ArgumentNullException(nameof(dao));
            _dao = dao;
        }

        public async Task HandleAsync(BookCreated @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            var book = await _dao.GetOrAddAsync(@event.SourceId).ConfigureAwait(false);

            book.Title = @event.Title;
            book.Author = @event.Author;
        }

        public async Task HandleAsync(BookPriceSet @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            var book = await _dao.GetOrAddAsync(@event.SourceId).ConfigureAwait(false);

            book.Price = @event.Price;
        }

        public Task HandleAsync(BookRemoved @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            return Task.Run(() => _dao.Remove(@event.SourceId));
        }
    }
}
