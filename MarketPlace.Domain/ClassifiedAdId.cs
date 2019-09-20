using System;

namespace MarketPlace.Domain
{
    public class ClassifiedAdId
    {
        private readonly Guid _value;

        public ClassifiedAdId(Guid value)
        {
            if (value == default)
                throw new ArgumentException("Classified Ad Id cannot be empty", nameof(value));

            _value = value;
        }
    }
}