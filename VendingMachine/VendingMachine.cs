namespace VendingMachines
{
    public enum Coin
    {
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
            return true;
        }
    }
}