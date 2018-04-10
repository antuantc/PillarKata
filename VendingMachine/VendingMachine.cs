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
        private int totalValue;
        private int invalidValue;

        public VendingMachine()
        {
            totalValue = 0;
            invalidValue = 0;
        }

        public bool InsertCoin(Coin coin)
        {
            if (coin.Equals(Coin.Nickle) || coin.Equals(Coin.Dime) || coin.Equals(Coin.Quarter))
            {
                totalValue += (int)coin;
                return true;
            }
            else
            {
                invalidValue += (int)coin;
                return false;
            }
        }
    }
}