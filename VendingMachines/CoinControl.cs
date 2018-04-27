using System.Collections.Generic;
using System.Linq;

namespace VendingMachines
{
    public class CoinControl
    {
        private static Coin _penny = new Coin();
        private static Coin _nickel = new Coin(5.0, 21.21);
        private static Coin _dime = new Coin(2.268, 17.91);
        private static Coin _quarter = new Coin(5.67, 24.26);
        private static Coin _halfDollar = new Coin(11.34, 30.61);
        private static Coin _dollar = new Coin(8.1, 26.49);

        private List<Coin> _coinBank;
        private List<Coin> _returnedCoins;

        public CoinControl()
        {
            _coinBank = new List<Coin>();
            _returnedCoins = new List<Coin>();

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

        public bool AddCoin(double weight, double size)
        {
            Coin coin = new Coin(weight, size);
            return AddCoin(coin);
        }

        public bool AddCoin(Coin coin)
        {
            _coinBank.Add(coin);
            return true;
        }

        public bool ReturnCoins(decimal totalValue)
        {
            _returnedCoins = MakeChange(ref _coinBank, totalValue);
            return true;
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
