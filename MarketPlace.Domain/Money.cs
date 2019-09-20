using System;
using MarketPlace.Framework;

namespace MarketPlace.Domain
{
    public class Money: Value<Money>
    {
        public decimal Amount { get; }
        public Money(decimal amount) => Amount = amount;

    }
}