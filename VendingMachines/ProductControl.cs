using System.Collections.Generic;

namespace VendingMachines
{
    public class ProductControl
    {
        public const string COLA = "Cola";
        public const string CHIPS = "Chips";
        public const string CANDY = "Candy";

        private List<Product> _productInStock;

        public ProductControl()
        {
            _productInStock = new List<Product>();

            _productInStock.Add(new Product(CANDY, 5, .65m));
            _productInStock.Add(new Product(COLA, 5, 1m));
            _productInStock.Add(new Product(CHIPS, 5, .5m));
        }

        public List<Product> productInStock
        {
            get
            {
                return _productInStock;
            }
        }

        public bool AddProduct(string name, int quantity, decimal price)
        {
            Product prod = new Product(name, quantity, price);
            return AddProduct(prod);
        }

        public bool AddProduct(Product prod)
        {
            _productInStock.Add(prod);
            return true;
        }
    }
}
