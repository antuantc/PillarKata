using System;
using Xunit;
using VendingMachines;
using System.Collections.Generic;


namespace VendingMachineTests
{
    public class VendingMachineUnitTests
    {
        private VendingMachine _vending;
        private static Coin _penny = new Coin();
        private static Coin _nickel = new Coin(5.0, 21.21);
        private static Coin _dime = new Coin(2.268, 17.91);
        private static Coin _quarter = new Coin(5.67, 24.26);
        private static Coin _halfDollar = new Coin(11.34, 30.61);
        private static Coin _dollar = new Coin(8.1, 26.49);

        private static Coin[] validCoins = { _nickel, _dime, _quarter };
        private static Coin[] inValidCoins = { _penny, _halfDollar, _dollar };

        public VendingMachineUnitTests()
        {
            _vending = new VendingMachine();
        }

        [Fact]
        public void AcceptADimeAsAValidCoin()
        {
            Assert.True(_vending.InsertCoin(_dime));
        }

        [Fact]
        public void AcceptValidCoins()
        {
            var accept = _vending.InsertCoin(_nickel) && _vending.InsertCoin(_dime) && _vending.InsertCoin(_quarter);
            Assert.True(accept);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetValidCoinsFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public void AcceptValidCoins(Coin coin1, Coin coin2, Coin coin3)
        {
            Assert.True(_vending.InsertCoin(coin1));
            Assert.True(_vending.InsertCoin(coin2));
            Assert.True(_vending.InsertCoin(coin3));
        }

        [Fact]
        public void AcceptAPennyAsAnInValidCoin()
        {
            var accept = _vending.InsertCoin(_penny);
            Assert.False(accept);
        }

        [Fact]
        public void DoNotAcceptInValidCoins()
        {
            var accept = _vending.InsertCoin(_penny) || _vending.InsertCoin(_halfDollar) || _vending.InsertCoin(_dollar);
            Assert.False(accept);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetInValidCoinsFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public void DoNotAcceptInValidCoins(Coin coin1, Coin coin2, Coin coin3)
        {
            Assert.False(_vending.InsertCoin(coin1));
            Assert.False(_vending.InsertCoin(coin2));
            Assert.False(_vending.InsertCoin(coin3));
        }

        [Fact]
        public void Insert75CentTotal()
        {
            _vending.InsertCoin(_quarter);
            _vending.InsertCoin(_quarter);
            _vending.InsertCoin(_quarter);
            Assert.Equal(.75m, _vending.totalValue);
        }

        [Fact]
        public void DisplayInsertCoin()
        {
            Assert.Equal(VendingMachine.INSERTCOIN, _vending.display);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetValidCoinsFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public void DisplayTotalInsertedCoins(Coin coin1, Coin coin2, Coin coin3)
        {
            _vending.InsertCoin(coin1);
            _vending.InsertCoin(coin2);
            _vending.InsertCoin(coin3);
            decimal total = coin1.coinValue + coin2.coinValue + coin3.coinValue;
            Assert.Equal(total.ToString("C2"), _vending.display);
        }

        [Fact]
        public void SelectACoke()
        {
            _vending.SelectProduct(VendingMachine.COLA);
            Assert.Equal(VendingMachine.COLA, _vending.selectedProduct.name);
        }

        [Theory]
        [InlineData(VendingMachine.CANDY)]
        [InlineData(VendingMachine.CHIPS)]
        [InlineData(VendingMachine.COLA)]
        public void SelectAProduct(string product)
        {
            _vending.SelectProduct(product);
            Assert.Equal(product, _vending.selectedProduct.name);
        }

        [Fact]
        public void SelectACokeWithNotEnoughMoney_ReturnsFalse()
        {
            Assert.False(_vending.SelectProduct(VendingMachine.COLA));
        }

        [Fact]
        public void SelectACokeWithNotEnoughMoney_DisplaysPrice()
        {
            _vending.SelectProduct(VendingMachine.COLA);
            Assert.Equal("PRICE " + _vending.selectedProduct.price.ToString("C2"), _vending.display);
        }

        [Fact]
        public void SelectACokeWithEnoughMoney_ReturnsTrue()
        {
            _vending.InsertCoin(_quarter);
            _vending.InsertCoin(_quarter);
            _vending.InsertCoin(_quarter);
            _vending.InsertCoin(_quarter);
            Assert.True(_vending.SelectProduct(VendingMachine.COLA));
        }

        [Fact]
        public void SelectACokeWithEnoughMoney_DisplaysThankYou()
        {
            _vending.InsertCoin(_quarter);
            _vending.InsertCoin(_quarter);
            _vending.InsertCoin(_quarter);
            _vending.InsertCoin(_quarter);
            _vending.SelectProduct(VendingMachine.COLA);
            Assert.Equal(VendingMachine.THANKYOU, _vending.display);
        }

        [Fact]
        public void SelectACokeWithNoMoney_DisplaysInsertCoin()
        {
            _vending.SelectProduct(VendingMachine.COLA);
            _vending.CheckDisplay();
            Assert.Equal(VendingMachine.INSERTCOIN, _vending.display);
        }

        [Fact]
        public void SelectACokeWithNotEnoughMoney_DisplaysCurrentAmount()
        {
            _vending.InsertCoin(_quarter);
            _vending.SelectProduct(VendingMachine.COLA);
            _vending.CheckDisplay();
            Assert.Equal(_quarter.coinValue.ToString("C2"), _vending.display);
        }

        [Fact]
        public void SelectAChipsWithMoreThanEnoughMoney_MakeCorrectChange()
        {
            _vending.InsertCoin(_quarter);
            _vending.InsertCoin(_quarter);
            _vending.InsertCoin(_quarter);
            _vending.SelectProduct(VendingMachine.CHIPS);
            _vending.ReturnCoins();
            Assert.Equal(_quarter.coinValue, _vending.returnTotalValue);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetValidCoinsAndAProductFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public void MakeCorrectChange(Coin coin1, Coin coin2, Coin coin3, Coin coin4, Coin coin5, Coin coin6, string product)
        {
            decimal change = 0;
            decimal total = coin1.coinValue + coin2.coinValue + coin3.coinValue + coin4.coinValue + coin5.coinValue + coin6.coinValue;
            _vending.InsertCoin(coin1);
            _vending.InsertCoin(coin2);
            _vending.InsertCoin(coin3);
            _vending.InsertCoin(coin4);
            _vending.InsertCoin(coin5);
            _vending.InsertCoin(coin6);
            _vending.SelectProduct(product);
            _vending.ReturnCoins();

            change = total - _vending.selectedProduct.price;
            Assert.Equal(change, _vending.returnTotalValue);
        }

        [Fact]
        public void InsertAQuarterAndReturnTheQuarter()
        {
            _vending.InsertCoin(_quarter);
            _vending.ReturnCoins();
            Assert.Equal(_quarter.coinValue, _vending.returnTotalValue);
        }

        [Fact]
        public void InsertAQuarterAndReturnTheQuarter_DisplaysInsertCoin()
        {
            _vending.InsertCoin(_quarter);
            _vending.ReturnCoins();
            Assert.Equal(VendingMachine.INSERTCOIN, _vending.display);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetValidCoinsFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public void InsertCoinsAndReturnTheCoins(Coin coin1, Coin coin2, Coin coin3)
        {
            _vending.InsertCoin(coin1);
            _vending.InsertCoin(coin2);
            _vending.InsertCoin(coin3);
            _vending.ReturnCoins();
            decimal total = coin1.coinValue + coin2.coinValue + coin3.coinValue;
            Assert.Equal(total, _vending.returnTotalValue);
        }

        [Fact]
        public void SelectSoldOutProduct_DisplaysSoldOut()
        {
            for (int i = 0; i < 30; i++)
            {
                _vending.InsertCoin(_quarter);
            }

            for (int i = 0; i < 6; i++)
            {
                _vending.SelectProduct(VendingMachine.COLA);
            }
            Assert.Equal(VendingMachine.SOLDOUT, _vending.display);
        }

        [Fact]
        public void SelectSoldOutProductCheckDisplayAgain_DisplaysTotal()
        {
            for (int i = 0; i < 30; i++)
            {
                _vending.InsertCoin(_quarter);
            }

            for (int i = 0; i < 6; i++)
            {
                _vending.SelectProduct(VendingMachine.CANDY);
            }

            _vending.CheckDisplay();

            Assert.Equal(_vending.totalValue.ToString("C2"), _vending.display);
        }
    }
}
