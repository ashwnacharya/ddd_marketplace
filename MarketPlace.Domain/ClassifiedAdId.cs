using System;
using MarketPlace.Framework;

namespace MarketPlace.Domain
{
    public class ClassifiedAdId: Value<ClassifiedAdId>
    {
        public Guid Value { get; }

        public ClassifiedAdId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException("Classified ad id cannot be empty", nameof(value));

            Value = value;
        }

        public static implicit operator Guid(ClassifiedAdId self) => self.Value;

        public static implicit operator ClassifiedAdId(string value) 
            => new ClassifiedAdId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}