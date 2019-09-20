using System;
using MarketPlace.Framework;

namespace MarketPlace.Domain
{
    public class UserId: Value<UserId>
    {
        private readonly Guid _value;

        public UserId(Guid value) => _value = value;

    }
}