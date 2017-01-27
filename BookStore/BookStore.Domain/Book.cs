using BookStore.Core;
using BookStore.Core.EventSourcing;
using BookStore.Domain.Events;
using System;
using System.Collections.Generic;

namespace BookStore.Domain
{
    public class Book : AggregateRoot
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public decimal Price { get; private set; }
        public bool IsRemoved { get; private set; }

        public Book(string title, string author)
            : base(Guid.NewGuid())
        {
            if (title == null) throw new ArgumentNullException(nameof(title));
            if (author == null) throw new ArgumentNullException(nameof(author));

            Raise(new BookCreated
            {
                Title = title,
                Author = author
            });
        }

        public Book(Guid id, IEnumerable<IVersionedEvent> history)
            : this(id)
        {
            Rehydrate(history);
        }

        public void SetPrice(decimal price)
        {
            if (price < 0m) throw new ArgumentOutOfRangeException(nameof(price));

            if (price != Price)
            {
                Raise(new BookPriceSet
                {
                    Price = price
                });
            }
        }

        public void Remove()
        {
            Raise(new BookRemoved());
        }

        protected Book(Guid id)
            : base(id)
        {
            Handle<BookCreated>(OnBookCreated);
            Handle<BookPriceSet>(OnBookPriceSet);
            Handle<BookRemoved>(OnBookRemoved);
        }

        private void OnBookRemoved(BookRemoved @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));
            IsRemoved = true;
        }

        private void OnBookPriceSet(BookPriceSet @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));
            Price = @event.Price;
        }

        private void OnBookCreated(BookCreated @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));
            Title = @event.Title;
            Author = @event.Author;
        }
    }
}
