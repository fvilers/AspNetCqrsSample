using System;
using System.Collections.Generic;

namespace BookStore.Core
{
    public abstract class Entity : IEntity, IEquatable<Entity>
    {
        public Guid Id { get; }

        protected Entity(Guid id)
        {
            if (id.Equals(default(Guid))) throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        public bool Equals(Entity other)
        {
            if (ReferenceEquals(null, other)) return false;

            return EqualityComparer<Guid>.Default.Equals(Id, other.Id);
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<Guid>.Default.GetHashCode(Id);
        }
    }
}