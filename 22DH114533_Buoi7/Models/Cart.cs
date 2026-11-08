using System.Collections.Generic;
using System.Linq;

namespace DBSportStore.Models
{
    // A session-safe snapshot of one product in the guest cart.
    public class CartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int StockQuantity { get; set; }
        public decimal LineTotal { get { return UnitPrice * Quantity; } }
    }

    public class Cart
    {
        private readonly List<CartItem> items = new List<CartItem>();

        public IEnumerable<CartItem> Items { get { return items; } }

        public bool Add(Product product)
        {
            if (product == null || !product.IsActive || product.StockQuantity <= 0)
                return false;

            var item = items.FirstOrDefault(x => x.ProductID == product.ProductID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    ImageUrl = product.ImageUrl,
                    UnitPrice = product.Price,
                    Quantity = 1,
                    StockQuantity = product.StockQuantity
                });
                return true;
            }

            item.StockQuantity = product.StockQuantity;
            if (item.Quantity >= product.StockQuantity)
                return false;

            item.Quantity++;
            return true;
        }

        public bool Update(int productId, int quantity, int currentStock)
        {
            var item = items.FirstOrDefault(x => x.ProductID == productId);
            if (item == null || quantity <= 0 || quantity > currentStock)
                return false;

            item.Quantity = quantity;
            item.StockQuantity = currentStock;
            return true;
        }

        public void Remove(int productId)
        {
            items.RemoveAll(x => x.ProductID == productId);
        }

        public int TotalQuantity() { return items.Sum(x => x.Quantity); }
        public decimal TotalMoney() { return items.Sum(x => x.LineTotal); }
        public void Clear() { items.Clear(); }
    }
}
