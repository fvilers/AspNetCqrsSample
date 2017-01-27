using BookStore.Domain.Commands;
using System;

namespace BookStore.Api.Models
{
    internal static class Extensions
    {
        public static CreateBook ToCommand(this CreateBookModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var command = new CreateBook
            {
                Title = model.Title,
                Author = model.Author,
                Price = model.Price
            };

            return command;
        }

        public static SetBookPrice ToCommand(this UpdateBookModel model, Guid bookId)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var command = new SetBookPrice
            {
                BookId = bookId,
                Price = model.Price
            };

            return command;
        }
    }
}
