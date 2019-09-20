using Xunit;
using MarketPlace.Domain;


namespace MarketPlace.Tests
{
    public class MoneyTest
    {
        [Fact]
        public void Money_objects_with_same_amount_should_be_equal()
        {
            var firstAmount = new Money(5);
            var secondAmount = new Money(5);

            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        public void Sum_gives_full_amount()
        {
            var coin1 = new Money(1);
            var coin2 = new Money(2);
            var coin3 = new Money(2);

            var bankNote = new Money(5);

            Assert.Equal(bankNote, coin1 + coin2 + coin3);
        }
    }
}