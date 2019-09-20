using System;
using Xunit;
using MarketPlace.Domain;


namespace MarketPlace.Tests
{
    public class MoneyTest
    {
        private static readonly ICurrencyLookup lookup = new FakeCurrencyLookup();

        [Fact]
        public void Money_objects_with_same_amount_should_be_equal()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", lookup);
            var secondAmount = Money.FromDecimal(5, "EUR", lookup);

            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        public void Sum_gives_full_amount()
        {
            var coin1 = Money.FromDecimal(1, "EUR", lookup);
            var coin2 = Money.FromDecimal(2, "EUR", lookup);
            var coin3 = Money.FromDecimal(2, "EUR", lookup);

            var bankNote = Money.FromDecimal(5, "EUR", lookup);

            Assert.Equal(bankNote, coin1 + coin2 + coin3);
        }


        [Fact]
        public void Two_of_same_amount_but_different_Currencies_should_not_be_equal()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", lookup);
            var secondAmount = Money.FromDecimal(5, "USD", lookup);

            Assert.NotEqual(firstAmount, secondAmount);
        }


        [Fact]
        public void FromString_and_FromDecimal_should_be_equal()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", lookup);
            var secondAmount = Money.FromString("5.00", "EUR", lookup);

            Assert.Equal(firstAmount, secondAmount);
        }


        [Fact]
        public void Unused_currency_should_not_be_allowed()
        {
            Assert.Throws<ArgumentException>(() =>
                Money.FromDecimal(100, "INR", lookup)
            );
        }

        [Fact]
        public void Unknown_currency_should_not_be_allowed()
        {
            Assert.Throws<ArgumentException>(() =>
                Money.FromDecimal(100, "WHAT?", lookup)
            );
        }

        [Fact]
        public void Throw_when_too_many_decimal_places()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Money.FromDecimal(100.123m, "EUR", lookup)
            );
        }

        [Fact]
        public void Throws_on_adding_different_currencies()
        {
            var firstAmount = Money.FromDecimal(5, "USD", lookup);
            var secondAmount = Money.FromDecimal(5, "EUR", lookup);

            Assert.Throws<CurrencyMismatchException>(() =>
                firstAmount + secondAmount
            );
        }

        [Fact]
        public void Throws_on_substracting_different_currencies()
        {
            var firstAmount = Money.FromDecimal(5, "USD", lookup);
            var secondAmount = Money.FromDecimal(5, "EUR", lookup);

            Assert.Throws<CurrencyMismatchException>(() =>
                firstAmount - secondAmount
            );
        }
    }
}