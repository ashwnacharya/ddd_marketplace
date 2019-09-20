using Xunit;
using MarketPlace.Domain;


namespace MarketPlace.Tests
{
    public class MoneyTest
    {
        [Fact]
        public void Money_objects_with_same_amount_should_be_equal()
        {
            var firstAmount = Money.FromDecimal(5);
            var secondAmount = Money.FromDecimal(5);

            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        public void Sum_gives_full_amount()
        {
            var coin1 = Money.FromDecimal(1);
            var coin2 = Money.FromDecimal(2);
            var coin3 = Money.FromDecimal(2);

            var bankNote = Money.FromDecimal(5);

            Assert.Equal(bankNote, coin1 + coin2 + coin3);
        }
    }
}