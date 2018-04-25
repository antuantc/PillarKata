using System.Collections.Generic;

namespace VendingMachines
{
    public enum Coin
    {
        None = 0,
        Penny = 1,
        Nickle = 5,
        Dime = 10,
        Quarter = 25
    }

    public enum Product
    {
        None = 0,
        Cola = 100,
        Chips = 50,
        Candy = 65
    }

    public class VendingMachine
    {
        public const string CONST_THANKYOU = "THANK YOU";
        public const string CONST_INSERTCOIN = "INSERT COIN";
        public const string CONST_SOLDOUT = "SOLD OUT";

        private int _totalValue;
        private int _coinReturnValue;
        private string _previousDisplay;
        private string _display;
        private Product _selectedProduct;
        private Dictionary<Product, int> _productStock;

        //static void Main(string[] args)
        //{}

        public VendingMachine()
        {
            _totalValue = 0;
            _coinReturnValue = 0;
            _display = CONST_INSERTCOIN;
            _selectedProduct = Product.None;

            //Initialize product to have 5 each
            _productStock = new Dictionary<Product, int>();
            _productStock.Add(Product.Candy, 5);
            _productStock.Add(Product.Cola, 5);
            _productStock.Add(Product.Chips, 5);
        }

        public int totalValue
        {
            get
            {
                return _totalValue;
            }
        }

        public int coinReturnValue
        {
            get
            {
                return _coinReturnValue;
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

        public bool InsertCoin(Coin coin)
        {
            //Valid coins
            if (coin.Equals(Coin.Nickle) || coin.Equals(Coin.Dime) || coin.Equals(Coin.Quarter))
            {
                _totalValue += (int)coin;
                _display = (_totalValue/100m).ToString("C2");
                _previousDisplay = _display;
                return true;
            }
            //Invalid coins
            else
            {
                _coinReturnValue += (int)coin;
                return false;
            }
        }

        public bool SelectProduct(Product product)
        {
            int price = (int)product;
            _selectedProduct = product;

            if (product != Product.None)
            {
                //Enough money for selected product
                if (_totalValue >= price)
                {
                    if (_productStock[product] > 0)
                    {
                        _totalValue = _totalValue - price;
                        _display = CONST_THANKYOU;
                        _previousDisplay = _display;
                        _productStock[product] = _productStock[product] - 1;
                        return true;
                    }
                    else
                    {
                        _display = CONST_SOLDOUT;
                        _previousDisplay = _display;
                        return false;
                    }
                }
                //Not enough money for selected product
                else
                {
                    _display = "PRICE " + (price / 100m).ToString("C2");
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
            if (_previousDisplay.Equals((_totalValue / 100m).ToString("C2")) ||
                _previousDisplay.Equals(CONST_THANKYOU))
            {
                _display = CONST_INSERTCOIN;
                _previousDisplay = _display;
            }
            //Previous display was the price of the selected item
            else if (_previousDisplay.Equals("PRICE " + ((int)_selectedProduct / 100m).ToString("C2")) ||
                _previousDisplay.Equals(CONST_SOLDOUT))
            {
                if(_totalValue > 0)
                {
                    _display = (_totalValue / 100m).ToString("C2");
                    _previousDisplay = _display;
                }
                else
                {
                    _display = CONST_INSERTCOIN;
                    _previousDisplay = _display;
                }
            }
        }

        public void ReturnCoins()
        {
            _coinReturnValue = _totalValue;
            _totalValue = 0;
            _display = CONST_INSERTCOIN;
            _previousDisplay = _display;
        }
    }
}