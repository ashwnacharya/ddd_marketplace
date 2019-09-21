using System;
using MarketPlace.Framework;

namespace MarketPlace.Domain
{
    public class ClassifiedAdId: Value<ClassifiedAdId>
    {
        private readonly Guid _value;

        public ClassifiedAdId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException("Classified ad id cannot be empty", nameof(value));

            _value = value;
        }

        public static implicit operator Guid(ClassifiedAdId self) => self._value;

        public static implicit operator ClassifiedAdId(string value) 
            => new ClassifiedAdId(Guid.Parse(value));
    }
}