using System;
using MarketPlace.Framework;

namespace MarketPlace.Domain
{
    public class Money: Value<Money>
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        public static string DefaultCurrency = "EUR";

        public static Money FromDecimal(
            decimal amount, string currencyCode, ICurrencyLookup lookup)
                => new Money(amount, currencyCode, lookup);

        public static Money FromString(
            string amount, string currencyCode, ICurrencyLookup lookup) => 
                new Money(decimal.Parse(amount), currencyCode, lookup);

        protected Money(decimal amount, string currencyCode, ICurrencyLookup lookup)
        {
            if(string.IsNullOrEmpty(currencyCode))
                throw new ArgumentNullException(
                    "Currency code must be specified", nameof(currencyCode));

            var currency = lookup.FindCurrency(currencyCode);

            if(!currency.InUse)
                throw new ArgumentException($"Currency code {currencyCode} is not valid");

            if (decimal.Round(amount, currency.DecimalPlaces) != amount)
                throw new ArgumentOutOfRangeException(
                    "Amount cannot have more than two decimals", nameof(amount));

            Amount = amount;
            Currency = currency;
        }

        protected Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }


        public Money Add(Money summand)
        {
            if (Currency != summand.Currency)
                throw new CurrencyMismatchException(
                    "Cannot sum amounts with different currencies");

            return new Money(Amount + summand.Amount, Currency);
        }


        public Money Subtract(Money subtrahend)
        {
            if (Currency != subtrahend.Currency)
                throw new CurrencyMismatchException(
                    "Cannot subtract amounts with different currencies");

            return new Money(Amount - subtrahend.Amount, Currency);
        }


        public static Money operator +(
            Money summand1, Money summand2) =>
                summand1.Add(summand2);


        public static Money operator -(
            Money minuend, Money subtrahend) =>
                minuend.Subtract(subtrahend);


    }

    public class CurrencyMismatchException: Exception
    {
        public CurrencyMismatchException(string message): base(message)
        {

        }
    }
}