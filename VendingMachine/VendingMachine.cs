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

        public VendingMachine()
        {
            _totalValue = 0;
            _invalidValue = 0;
        }

        public int totalValue
        {
            get
            {
                return _totalValue;
            }
        }

        public bool InsertCoin(Coin coin)
        {
            if (coin.Equals(Coin.Nickle) || coin.Equals(Coin.Dime) || coin.Equals(Coin.Quarter))
            {
                _totalValue += (int)coin;
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