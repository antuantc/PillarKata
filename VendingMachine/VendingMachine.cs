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
        public VendingMachine()
        {
        }

        public bool InsertCoin(Coin coin)
        {
            if (coin.Equals(Coin.Penny))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}