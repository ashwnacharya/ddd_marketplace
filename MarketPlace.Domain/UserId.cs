using System;

namespace MarketPlace.Domain
{
    public class UserId
    {
        private readonly Guid _value;

        public UserId(Guid value)
        {
            if (value == default)
                throw new ArgumentException("User id cannot be empty", nameof(value));

            _value = value;

        }
    }
}