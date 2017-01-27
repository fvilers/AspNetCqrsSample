using System;
using System.Collections.Generic;

namespace BookStore.Core.Collections.Generic
{
    public static class Extensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (range == null) throw new ArgumentNullException(nameof(range));

            foreach (var item in range)
            {
                collection.Add(item);
            }
        }
    }
}
