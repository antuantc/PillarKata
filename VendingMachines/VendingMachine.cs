
namespace VendingMachines
{
    public class VendingMachine
    {
        public const string THANKYOU = "THANK YOU";
        public const string INSERTCOIN = "INSERT COIN";
        public const string SOLDOUT = "SOLD OUT";

        private decimal _totalValue;
        private Product _selectedProduct;
        private string _previousDisplay;
        private string _display;
        private CoinControl _coinControl;
        private ProductControl _productControl;
       
        public VendingMachine()
        {
            _totalValue = 0;
            _display = INSERTCOIN;
            _selectedProduct = null;
            _coinControl = new CoinControl();
            _productControl = new ProductControl();
        }

        public decimal totalValue
        {
            get
            {
                return _totalValue;
            }
        }

        public decimal coinBankTotalValue
        {
            get
            {
                return _coinControl.coinBankTotalValue;
            }
        }

        public decimal returnTotalValue
        {
            get
            {
                return _coinControl.returnTotalValue;
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
                _productControl.AddProduct(prod);
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
                _coinControl.AddCoin(coin);
                _totalValue += coin.coinValue;
                _display = (_totalValue).ToString("C2");
                _previousDisplay = _display;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SelectProduct(string product)
        {
            var prod = _productControl.productInStock.Find(x => x.name.Equals(product));
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
            _coinControl.ReturnCoins(_totalValue);
            _totalValue = 0;
            _display = INSERTCOIN;
            _previousDisplay = _display;
        }
    }
}