using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using VendingMachines;

namespace VendingMachineTests
{
    public class TestDataGenerator : IEnumerable<object[]>
    {
        private static Coin _penny = new Coin();
        private static Coin _nickel = new Coin(5.0, 21.21);
        private static Coin _dime = new Coin(2.268, 17.91);
        private static Coin _quarter = new Coin(5.67, 24.26);
        private static Coin _halfDollar = new Coin(11.34, 30.61);
        private static Coin _dollar = new Coin(8.1, 26.49);

        public static IEnumerable<object[]> GetValidCoinsFromDataGenerator()
        {
            yield return new object[]
            {
                _nickel,
                _dime,
                _quarter
            };
        }

        public static IEnumerable<object[]> GetInValidCoinsFromDataGenerator()
        {
            yield return new object[]
            {
                _penny,
                _halfDollar,
                _dollar
            };
        }

        public static IEnumerable<object[]> GetValidCoinsAndAProductFromDataGenerator()
        {
            yield return new object[]
            {
                _nickel,
                _dime,
                _quarter,
                _quarter,
                _quarter,
                _quarter,
                Product.Candy
            };
        }

        public IEnumerator<object[]> GetEnumerator() => GetValidCoinsFromDataGenerator().GetEnumerator();

        IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
