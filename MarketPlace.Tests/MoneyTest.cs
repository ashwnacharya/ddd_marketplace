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
    }
}