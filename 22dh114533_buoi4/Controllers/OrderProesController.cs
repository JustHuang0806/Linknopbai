using DBSportStore.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DBSportStore.Controllers
{
    public class OrderProesController : Controller
    {
        private readonly DBSportStoreEntities db = new DBSportStoreEntities();
        public ActionResult Index() { return View(db.OrderProes.Include(o => o.Customer).OrderByDescending(o => o.OrderDate).ToList()); }
        public ActionResult Details(int? id) { if (id == null) return new HttpStatusCodeResult(400); var item = db.OrderProes.Include(o => o.Customer).SingleOrDefault(o => o.OrderID == id); return item == null ? (ActionResult)HttpNotFound() : View(item); }
        public ActionResult Create() { PopulateCustomers(); return View(new OrderPro { OrderDate = System.DateTime.Now, TotalAmount = 0, Status = "Pending" }); }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(OrderPro order) { if (ModelState.IsValid) { db.OrderProes.Add(order); db.SaveChanges(); return RedirectToAction("Index"); } PopulateCustomers(order.CustomerID); return View(order); }
        public ActionResult Edit(int? id) { if (id == null) return new HttpStatusCodeResult(400); var item = db.OrderProes.Find(id); if (item == null) return HttpNotFound(); PopulateCustomers(item.CustomerID); return View(item); }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(OrderPro order) { if (ModelState.IsValid) { var item = db.OrderProes.Find(order.OrderID); if (item == null) return HttpNotFound(); item.OrderDate = order.OrderDate; item.CustomerID = order.CustomerID; item.ShippingAddress = order.ShippingAddress; item.TotalAmount = order.TotalAmount; item.Status = order.Status; db.SaveChanges(); return RedirectToAction("Index"); } PopulateCustomers(order.CustomerID); return View(order); }
        public ActionResult Delete(int? id) { if (id == null) return new HttpStatusCodeResult(400); var item = db.OrderProes.Include(o => o.Customer).SingleOrDefault(o => o.OrderID == id); return item == null ? (ActionResult)HttpNotFound() : View(item); }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) { var item = db.OrderProes.Find(id); if (item == null) return HttpNotFound(); var details = db.OrderDetails.Where(d => d.OrderID == id).ToList(); db.OrderDetails.RemoveRange(details); db.OrderProes.Remove(item); db.SaveChanges(); return RedirectToAction("Index"); }
        private void PopulateCustomers(int? selectedId = null) { ViewBag.CustomerID = new SelectList(db.Customers.OrderBy(c => c.FullName), "CustomerID", "FullName", selectedId); }
        protected override void Dispose(bool disposing) { if (disposing) db.Dispose(); base.Dispose(disposing); }
    }
}
