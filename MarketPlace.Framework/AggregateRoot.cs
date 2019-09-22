using System;
using System.Linq;
using System.Collections.Generic;

namespace MarketPlace.Framework
{
    public abstract class AggregateRoot<TId> where TId: Value<TId>
    {
        public TId Id { get; protected set; }

        private readonly List<object> _changes;

        protected AggregateRoot() => _changes = new List<object>();

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _changes.Add(@event);
        }

        protected abstract void When(object @event);

        public IEnumerable<object> GetChanges() => _changes.AsEnumerable();

        public void ClearChanges() => _changes.Clear();

        protected abstract void EnsureValidState();

    }
}