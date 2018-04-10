using System;
using Xunit;
using VendingMachines;


namespace VendingMachineTests
{
    public class VendingMachineUnitTests
    {
        [Fact]
        public void AcceptAValidCoin()
        {
            //arrange
            var vending = new VendingMachine();
            var coin = Coin.Dime;

            //act
            var accept = vending.InsertCoin(coin);

            //assert
            Assert.Equal(true, accept);
        }
    }
}
