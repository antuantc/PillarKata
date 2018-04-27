using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachines
{
    public class Product
    {
        private string _name;
        private int _quantity;
        private decimal _price;

        public Product()
        {
            _name = "";
            _quantity = 0;
            _price = 0m;
        }

        public Product(string name, int quantity, decimal price)
        {
            _name = name;
            _quantity = quantity;
            _price = price;
        }

        public decimal price
        {
            get
            {
                return _price;
            }
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        public int quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }
    }
}
