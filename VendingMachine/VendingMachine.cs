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

    public class VendingMachine
    {
        private int _totalValue;
        private int _invalidValue;
        private string _display;

        public VendingMachine()
        {
            _totalValue = 0;
            _invalidValue = 0;
            _display = "INSERT COIN";
        }

        public int totalValue
        {
            get
            {
                return _totalValue;
            }
        }

        public string display
        {
            get
            {
                return _display;
            }
        }

        public bool InsertCoin(Coin coin)
        {
            if (coin.Equals(Coin.Nickle) || coin.Equals(Coin.Dime) || coin.Equals(Coin.Quarter))
            {
                _totalValue += (int)coin;
                _display = (_totalValue/100m).ToString("C2");
                return true;
            }
            else
            {
                _invalidValue += (int)coin;
                return false;
            }
        }
    }
}