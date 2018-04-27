using System.Collections.Generic;
using System.Linq;

namespace VendingMachines
{
    //public enum Coin
    //{
    //    None = 0,
    //    Penny = 1,
    //    Nickle = 5,
    //    Dime = 10,
    //    Quarter = 25
    //}

    public enum Product
    {
        None = 0,
        Cola = 100,
        Chips = 50,
        Candy = 65
    }

    public class VendingMachine
    {
        public const string THANKYOU = "THANK YOU";
        public const string INSERTCOIN = "INSERT COIN";
        public const string SOLDOUT = "SOLD OUT";
        private static Coin _penny = new Coin();
        private static Coin _nickel = new Coin(5.0, 21.21);
        private static Coin _dime = new Coin(2.268, 17.91);
        private static Coin _quarter = new Coin(5.67, 24.26);
        private static Coin _halfDollar = new Coin(11.34, 30.61);
        private static Coin _dollar = new Coin(8.1, 26.49);

        private decimal _totalValue;
        //private int _coinReturnValue;
        private List<Coin> _insertedCoins;
        private List<Coin> _returnedCoins;

        private string _previousDisplay;
        private string _display;
        private Product _selectedProduct;
        private Dictionary<Product, int> _productStock;

        public VendingMachine()
        {
            _totalValue = 0;
            //_coinReturnValue = 0;
            _insertedCoins = new List<Coin>();
            _returnedCoins = new List<Coin>();
            _display = INSERTCOIN;
            _selectedProduct = Product.None;

            //Initialize product to have 5 each
            _productStock = new Dictionary<Product, int>();
            _productStock.Add(Product.Candy, 5);
            _productStock.Add(Product.Cola, 5);
            _productStock.Add(Product.Chips, 5);

            //Initialize coin bank
            _insertedCoins.Add(_nickel);
            _insertedCoins.Add(_nickel);
            _insertedCoins.Add(_nickel);
            _insertedCoins.Add(_nickel);
            _insertedCoins.Add(_nickel);
            _insertedCoins.Add(_nickel);
            _insertedCoins.Add(_nickel);
            _insertedCoins.Add(_nickel);
            _insertedCoins.Add(_nickel);
            _insertedCoins.Add(_dime);
            _insertedCoins.Add(_dime);
            _insertedCoins.Add(_dime);
            _insertedCoins.Add(_dime);
            _insertedCoins.Add(_dime);
            _insertedCoins.Add(_quarter);
            _insertedCoins.Add(_quarter);
            _insertedCoins.Add(_quarter);
            _insertedCoins.Add(_quarter);
        }

        public decimal totalValue
        {
            get
            {
                return _totalValue;
            }
        }

        public decimal coinListValue(List<Coin> coins)
        {
            decimal total = 0;
            foreach (Coin coin in coins)
            {
                total += coin.coinValue;
            }
            return total;
        }

        public decimal insertedTotalValue
        {
            get
            {
                return coinListValue(_insertedCoins);
            }
        }

        public decimal returnTotalValue
        {
            get
            {
                return coinListValue(_returnedCoins);
            }
        }

        public string display
        {
            get
            {
                return _display;
            }
        }

        public Product selectedProduct
        {
            get
            {
                return _selectedProduct;
            }
        }

        bool IsValidCoin(Coin coin)
        {
            if (coin.coinValue.Equals(.05m) || coin.coinValue.Equals(.1m) || coin.coinValue.Equals(.25m))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool InsertCoin(double weight, double size)
        {
            Coin coin = new Coin(weight, size);
            return InsertCoin(coin);
        }

        public bool InsertCoin(Coin coin)
        {
            //Valid coins
            if (IsValidCoin(coin))
            {
                _insertedCoins.Add(coin);
                _totalValue += coin.coinValue;
                _display = (_totalValue).ToString("C2");
                _previousDisplay = _display;
                return true;
            }
            //Invalid coins
            else
            {
                _returnedCoins.Add(coin);
                return false;
            }
        }

        public bool SelectProduct(Product product)
        {
            decimal price = ((decimal)(int)product / 100);
            _selectedProduct = product;

            if (product != Product.None)
            {
                //Enough money for selected product
                if (_totalValue >= price)
                {
                    if (_productStock[product] > 0)
                    {
                        _totalValue = _totalValue - price;
                        _display = THANKYOU;
                        _previousDisplay = _display;
                        _productStock[product] = _productStock[product] - 1;
                        return true;
                    }
                    else
                    {
                        _display = SOLDOUT;
                        _previousDisplay = _display;
                        return false;
                    }
                }
                //Not enough money for selected product
                else
                {
                    _display = "PRICE " + price.ToString("C2");
                    _previousDisplay = _display;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void CheckDisplay()
        {
            //Previous display was the total value
            if (_previousDisplay.Equals(_totalValue.ToString("C2")) ||
                _previousDisplay.Equals(THANKYOU))
            {
                _display = INSERTCOIN;
                _previousDisplay = _display;
            }
            //Previous display was the price of the selected item
            else if (_previousDisplay.Equals("PRICE " + ((int)_selectedProduct / 100m).ToString("C2")) ||
                _previousDisplay.Equals(SOLDOUT))
            {
                if(_totalValue > 0)
                {
                    _display = _totalValue.ToString("C2");
                    _previousDisplay = _display;
                }
                else
                {
                    _display = INSERTCOIN;
                    _previousDisplay = _display;
                }
            }
        }

        public void ReturnCoins()
        {
            _returnedCoins = MakeChange(ref _insertedCoins, _totalValue);
            _totalValue = 0;
            _display = INSERTCOIN;
            _previousDisplay = _display;
        }

        private List<Coin> MakeChange(ref List<Coin> coins, decimal value)
        {
            List<Coin> change = new List<Coin>();
            if (value > 0 && coins.Count > 0)
            {
                while (value >= 0.05m)
                {
                    List<Coin> coinList = coins.Where(x => x.coinValue <= value).OrderByDescending(x => x.coinValue).ToList();
                    var coin = coinList.First();
                    if (coin.coinValue <= value)
                    {
                        change.Add(coin);
                        coins.Remove(coin);
                        value -= coin.coinValue;
                    }
                }
            }
            return change;
        }
    }
}