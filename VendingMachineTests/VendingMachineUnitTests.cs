using System;
using Xunit;
using VendingMachines;
using System.Collections.Generic;


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
            Assert.True(accept);
        }

        [Fact]
        public void AcceptAPennyAsAnInValidCoin()
        {
            //arrange
            var coin = Coin.Penny;

            //act
            var accept = _vending.InsertCoin(coin);

            //assert
            Assert.False(accept);
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
            Assert.True(accept);
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

        [Fact]
        public void Insert75CentTotal()
        {
            //arrrange

            //act
            _vending.InsertCoin(Coin.Quarter);
            _vending.InsertCoin(Coin.Quarter);
            _vending.InsertCoin(Coin.Quarter);

            //assert
            Assert.Equal(75, _vending.totalValue);
        }

        [Fact]
        public void DisplayInsertCoin()
        {
            //arrrange

            //act

            //assert
            Assert.Equal(VendingMachine.CONST_INSERTCOIN, _vending.display);
        }

        [Theory]
        [InlineData(new Coin[] { Coin.Dime, Coin.Nickle })]
        [InlineData(new Coin[] { Coin.Dime, Coin.Nickle, Coin.Quarter })]
        public void DisplayTotalInsertedCoins(Coin [] coins)
        {
            //arrrange
            int total = 0;

            //act
            foreach(Coin coin in coins)
            {
                _vending.InsertCoin(coin);
                total += (int)coin;
            }

            //assert
            Assert.Equal((total / 100m).ToString("C2"),_vending.display);
        }

        [Fact]
        public void SelectACoke()
        {
            //arrrange

            //act
            _vending.SelectProduct(Product.Cola);

            //assert
            Assert.Equal("Cola", _vending.selectedProduct.ToString());
        }

        [Fact]
        public void SelectACokeWithNotEnoughMoney_ReturnsFalse()
        {
            //arrrange

            //act
            var select = _vending.SelectProduct(Product.Cola);

            //assert
            Assert.False(select);
        }

        [Fact]
        public void SelectACokeWithNotEnoughMoney_DisplaysPrice()
        {
            //arrrange

            //act
            var select = _vending.SelectProduct(Product.Cola);

            //assert
            Assert.Equal("PRICE " + ((int)Product.Cola / 100m).ToString("C2"), _vending.display);
        }

        [Fact]
        public void SelectACokeWithEnoughMoney_ReturnsTrue()
        {
            //arrrange

            //act
            _vending.InsertCoin(Coin.Quarter);
            _vending.InsertCoin(Coin.Quarter);
            _vending.InsertCoin(Coin.Quarter);
            _vending.InsertCoin(Coin.Quarter);
            var select = _vending.SelectProduct(Product.Cola);

            //assert
            Assert.True(select);
        }

        [Fact]
        public void SelectACokeWithEnoughMoney_DisplaysThankYou()
        {
            //arrrange

            //act
            _vending.InsertCoin(Coin.Quarter);
            _vending.InsertCoin(Coin.Quarter);
            _vending.InsertCoin(Coin.Quarter);
            _vending.InsertCoin(Coin.Quarter);
            _vending.SelectProduct(Product.Cola);

            //assert
            Assert.Equal(VendingMachine.CONST_THANKYOU, _vending.display);
        }

        [Fact]
        public void SelectACokeWithNoMoney_DisplaysInsertCoin()
        {
            //arrrange

            //act
            _vending.SelectProduct(Product.Cola);
            _vending.CheckDisplay();

            //assert
            Assert.Equal(VendingMachine.CONST_INSERTCOIN, _vending.display);
        }

        [Fact]
        public void SelectACokeWithNotEnoughMoney_DisplaysCurrentAmount()
        {
            //arrrange

            //act
            _vending.InsertCoin(Coin.Quarter);
            var select = _vending.SelectProduct(Product.Cola);
            _vending.CheckDisplay();

            //assert
            Assert.Equal(((int)Coin.Quarter / 100m).ToString("C2"), _vending.display);
        }
    }
}
