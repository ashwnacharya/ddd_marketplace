using System;
using MarketPlace.Framework;

namespace MarketPlace.Domain
{
    public class ClassifiedAdId: Value<ClassifiedAdId>
    {
        private readonly Guid _value;

        public ClassifiedAdId(Guid value) => _value = value;
    }
}