using BookStore.Core;
using BookStore.Core.Messaging.Handling;
using BookStore.Domain.Commands;
using BookStore.Domain.SpecialCases;
using System;
using System.Threading.Tasks;

namespace BookStore.Domain.CommandHandlers
{
    public class BookCommandHandler : ICommandHandler<CreateBook>, ICommandHandler<SetBookPrice>
    {
        private readonly IAggregateRepository<Book> _repository;

        public BookCommandHandler(IAggregateRepository<Book> repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            _repository = repository;
        }

        public Task HandleAsync(CreateBook command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var book = new Book(command.Title, command.Author);

            return _repository.StoreAsync(book, command.Id);
        }

        public async Task HandleAsync(SetBookPrice command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var book = await _repository.GetAsync(command.BookId).ConfigureAwait(false);

            if (book is BookNotFound)
            {
                return;
            }

            book.SetPrice(command.Price);
            await _repository.StoreAsync(book, command.Id).ConfigureAwait(false);
        }
    }
}
