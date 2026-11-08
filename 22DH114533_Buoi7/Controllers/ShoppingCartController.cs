using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DBSportStore.Models;

namespace DBSportStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly DBSportStoreEntities db = new DBSportStoreEntities();

        private Cart GetCart()
        {
            var cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult ShowCart()
        {
            return View(GetCart());
        }

        public ActionResult AddToCart(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductID == id && p.IsActive);
            if (product == null)
            {
                TempData["CartError"] = "Sản phẩm không tồn tại hoặc đã ngừng bán.";
                return RedirectToAction("Index", "Store");
            }

            if (!GetCart().Add(product))
                TempData["CartError"] = "Không thể thêm sản phẩm. Số lượng đã đạt giới hạn tồn kho.";

            return RedirectToAction("ShowCart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateQuantity(int productId, int quantity)
        {
            var product = db.Products.Find(productId);
            if (product == null || !product.IsActive)
            {
                TempData["CartError"] = "Sản phẩm không còn được bán.";
                return RedirectToAction("ShowCart");
            }

            if (!GetCart().Update(productId, quantity, product.StockQuantity))
                TempData["CartError"] = "Số lượng phải lớn hơn 0 và không vượt tồn kho hiện tại.";

            return RedirectToAction("ShowCart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int productId)
        {
            GetCart().Remove(productId);
            return RedirectToAction("ShowCart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClearCart()
        {
            GetCart().Clear();
            return RedirectToAction("ShowCart");
        }

        [ChildActionOnly]
        public ActionResult BagCart()
        {
            return PartialView(GetCart());
        }

        public ActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Items.Any())
                return RedirectToAction("ShowCart");

            return View(new CheckoutViewModel { Cart = cart });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutViewModel model)
        {
            var cart = GetCart();
            model.Cart = cart;
            if (!cart.Items.Any())
                return RedirectToAction("ShowCart");

            if (!ModelState.IsValid)
                return View(model);

            foreach (var item in cart.Items)
            {
                var product = db.Products.Find(item.ProductID);
                if (product == null || !product.IsActive || product.StockQuantity < item.Quantity)
                {
                    ModelState.AddModelError("", "Sản phẩm " + item.ProductName + " không còn đủ tồn kho.");
                    return View(model);
                }
            }

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var customer = new Customer
                    {
                        UserID = null,
                        FullName = model.FullName.Trim(),
                        Phone = model.Phone.Trim(),
                        Email = model.Email.Trim(),
                        Address = model.Address.Trim()
                    };
                    db.Customers.Add(customer);
                    db.SaveChanges();

                    var order = new OrderPro
                    {
                        OrderDate = DateTime.Now,
                        CustomerID = customer.CustomerID,
                        RecipientName = customer.FullName,
                        ShippingPhone = customer.Phone,
                        ShippingAddress = model.ShippingAddress.Trim(),
                        TotalAmount = cart.TotalMoney(),
                        Status = "Chờ xử lý"
                    };
                    db.OrderProes.Add(order);
                    db.SaveChanges();

                    foreach (var item in cart.Items.ToList())
                    {
                        // Guard the decrement in SQL so concurrent checkouts cannot oversell stock.
                        var changed = db.Database.ExecuteSqlCommand(
                            "UPDATE dbo.Product SET StockQuantity = StockQuantity - @p0 " +
                            "WHERE ProductID = @p1 AND IsActive = 1 AND StockQuantity >= @p0",
                            item.Quantity, item.ProductID);
                        if (changed != 1)
                            throw new InvalidOperationException("Tồn kho đã thay đổi trong lúc đặt hàng.");

                        db.OrderDetails.Add(new OrderDetail
                        {
                            OrderID = order.OrderID,
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice
                        });
                    }

                    db.SaveChanges();
                    transaction.Commit();
                    cart.Clear();
                    return RedirectToAction("OrderSuccess", new { id = order.OrderID });
                }
                catch
                {
                    transaction.Rollback();
                    ModelState.AddModelError("", "Có lỗi khi lưu đơn hàng. Vui lòng thử lại.");
                    return View(model);
                }
            }
        }

        public ActionResult OrderSuccess(int id)
        {
            var order = db.OrderProes
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails.Select(d => d.Product))
                .FirstOrDefault(o => o.OrderID == id);

            if (order == null)
                return HttpNotFound();

            return View(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
