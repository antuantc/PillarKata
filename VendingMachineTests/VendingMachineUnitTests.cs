using System;
using Xunit;
using VendingMachines;


namespace VendingMachineTests
{
    public class VendingMachineUnitTests
    {
        private VendingMachine _vending;

        public VendingMachineUnitTests()
        {
            _vending = new VendingMachine();
        }

        [Fact]
        public void AcceptADimeAsAValidCoin()
        {
            //arrange
            var coin = Coin.Dime;

            //act
            var accept = _vending.InsertCoin(coin);

            //assert
            Assert.Equal(true, accept);
        }

        [Fact]
        public void AcceptAPennyAsAnInValidCoin()
        {
            //arrange
            var coin = Coin.Penny;

            //act
            var accept = _vending.InsertCoin(coin);

            //assert
            Assert.Equal(false, accept);
        }

        [Fact]
        public void AcceptValidCoins()
        {
            //arrange
            var coin1 = Coin.Nickle;
            var coin2 = Coin.Dime;
            var coin3 = Coin.Quarter;

            //act
            var accept = _vending.InsertCoin(coin1) && _vending.InsertCoin(coin2) && _vending.InsertCoin(coin3);

            //assert
            Assert.Equal(true, accept);
        }

        [Theory]
        [InlineData(Coin.Nickle)]
        [InlineData(Coin.Dime)]
        [InlineData(Coin.Quarter)]
        public void ReturnTrueGivenValidCoins(Coin coin)
        {
            Assert.True(_vending.InsertCoin(coin));
        }

        [Theory]
        [InlineData(Coin.None)]
        [InlineData(Coin.Penny)]
        public void ReturnFalseGivenInValidCoins(Coin coin)
        {
            Assert.False(_vending.InsertCoin(coin));
        }
    }
}
