using System.Collections.Generic;
using System.Linq;

namespace VendingMachines
{
    //public enum Product
    //{
    //    None = 0,
    //    Cola = 100,
    //    Chips = 50,
    //    Candy = 65
    //}

    public class VendingMachine
    {
        public const string THANKYOU = "THANK YOU";
        public const string INSERTCOIN = "INSERT COIN";
        public const string SOLDOUT = "SOLD OUT";
        public const string COLA = "Cola";
        public const string CHIPS = "Chips";
        public const string CANDY = "Candy";

        private static Coin _penny = new Coin();
        private static Coin _nickel = new Coin(5.0, 21.21);
        private static Coin _dime = new Coin(2.268, 17.91);
        private static Coin _quarter = new Coin(5.67, 24.26);
        private static Coin _halfDollar = new Coin(11.34, 30.61);
        private static Coin _dollar = new Coin(8.1, 26.49);

        private decimal _totalValue;
        private List<Coin> _coinBank;
        private List<Coin> _returnedCoins;
        private Product _selectedProduct;
        private List<Product> _productInStock;
        private string _previousDisplay;
        private string _display;
        
        //private Dictionary<Product, int> _productStock;

        public VendingMachine()
        {
            _totalValue = 0;
            _coinBank = new List<Coin>();
            _returnedCoins = new List<Coin>();
            _display = INSERTCOIN;
            _selectedProduct = null;
            _productInStock = new List<Product>();

            //Initialize product in stock
            _productInStock.Add(new Product(CANDY, 5, .65m));
            _productInStock.Add(new Product(COLA, 5, 1m));
            _productInStock.Add(new Product(CHIPS, 5, .5m));

            //Initialize coin bank
            _coinBank.Add(_nickel);
            _coinBank.Add(_nickel);
            _coinBank.Add(_nickel);
            _coinBank.Add(_nickel);
            _coinBank.Add(_nickel);
            _coinBank.Add(_nickel);
            _coinBank.Add(_nickel);
            _coinBank.Add(_nickel);
            _coinBank.Add(_nickel);
            _coinBank.Add(_dime);
            _coinBank.Add(_dime);
            _coinBank.Add(_dime);
            _coinBank.Add(_dime);
            _coinBank.Add(_dime);
            _coinBank.Add(_quarter);
            _coinBank.Add(_quarter);
            _coinBank.Add(_quarter);
            _coinBank.Add(_quarter);
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

        public decimal coinBankTotalValue
        {
            get
            {
                return coinListValue(_coinBank);
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

        bool IsValidProduct(Product prod)
        {
            if (prod.name.Equals("Cola") || prod.name.Equals("Chips") || prod.name.Equals("Candy"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddProduct(string name, int quantity, decimal price)
        {
            Product prod = new Product(name, quantity, price);
            return AddProduct(prod);
        }

        public bool AddProduct(Product prod)
        {
            if (IsValidProduct(prod))
            {
                _productInStock.Add(prod);
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
            if (IsValidCoin(coin))
            {
                _coinBank.Add(coin);
                _totalValue += coin.coinValue;
                _display = (_totalValue).ToString("C2");
                _previousDisplay = _display;
                return true;
            }
            else
            {
                _returnedCoins.Add(coin);
                return false;
            }
        }

        public bool SelectProduct(string product)
        {
            var prod = _productInStock.Find(x => x.name.Equals(product));
            if (prod != null)
            {
                _selectedProduct = prod;

                if (_totalValue >= _selectedProduct.price)
                {
                    if (_selectedProduct.quantity > 0)
                    {
                        _totalValue = _totalValue - _selectedProduct.price;
                        _display = THANKYOU;
                        _previousDisplay = _display;
                        _selectedProduct.quantity = _selectedProduct.quantity - 1;
                        return true;
                    }
                    else
                    {
                        _display = SOLDOUT;
                        _previousDisplay = _display;
                        return false;
                    }
                }
                else
                {
                    _display = "PRICE " + _selectedProduct.price.ToString("C2");
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
            if (_previousDisplay.Equals(_totalValue.ToString("C2")) ||
                _previousDisplay.Equals(THANKYOU))
            {
                _display = INSERTCOIN;
                _previousDisplay = _display;
            }
            else if (_previousDisplay.Equals("PRICE " + _selectedProduct.price.ToString("C2")) ||
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
            _returnedCoins = MakeChange(ref _coinBank, _totalValue);
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