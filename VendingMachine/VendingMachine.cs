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

        private int _totalValue;
        private int _coinReturnValue;
        private string _previousDisplay;
        private string _display;
        private Product _selectedProduct;

        public VendingMachine()
        {
            _totalValue = 0;
            _coinReturnValue = 0;
            _display = CONST_INSERTCOIN;
            _selectedProduct = Product.None;
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
            if (coin.Equals(Coin.Nickle) || coin.Equals(Coin.Dime) || coin.Equals(Coin.Quarter))
            {
                _totalValue += (int)coin;
                _display = (_totalValue/100m).ToString("C2");
                _previousDisplay = _display;
                return true;
            }
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

            if (_totalValue >= price)
            {
                _coinReturnValue = _totalValue - price;
                _totalValue = 0;
                _display = CONST_THANKYOU;
                _previousDisplay = _display;
                return true;
            }
            else
            {
                _display = "PRICE " + (price / 100m).ToString("C2");
                _previousDisplay = _display;
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
            }
        }
    }
}