using Abc.MvcWebUI.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Abc.MvcWebUI.Models
{
    public class Cart
    {
        private List<CartLine> _cardLines = new List<CartLine>();

        public List<CartLine> CartLines
        {
            get { return _cardLines; }
        }

        public void AddProduct(Product product, int quantity)
        {
            var line = _cardLines.FirstOrDefault(x => x.Product.Id == product.Id);
            if (line == null)
            {
                _cardLines.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void DeleteProduct(Product product)
        {
            _cardLines.RemoveAll(x => x.Product.Id == product.Id);
        }

        public double Total()
        {
            return _cardLines.Sum(x => x.Product.Price * x.Quantity);
        }

        public void Clear()
        {
            _cardLines.Clear();
        }
    }
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}