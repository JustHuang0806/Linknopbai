using DBSportStore.Models;
using System.Linq;
using System.Web.Mvc;

namespace DBSportStore.Controllers
{
    public class CustomersController : Controller
    {
        private readonly DBSportStoreEntities db = new DBSportStoreEntities();
        public ActionResult Index() { return View(db.Customers.OrderBy(c => c.FullName).ToList()); }
        public ActionResult Details(int? id) { if (id == null) return new HttpStatusCodeResult(400); var item = db.Customers.Find(id); return item == null ? (ActionResult)HttpNotFound() : View(item); }
        public ActionResult Create() { return View(); }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer) { if (ModelState.IsValid) { db.Customers.Add(customer); db.SaveChanges(); return RedirectToAction("Index"); } return View(customer); }
        public ActionResult Edit(int? id) { if (id == null) return new HttpStatusCodeResult(400); var item = db.Customers.Find(id); return item == null ? (ActionResult)HttpNotFound() : View(item); }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer) { if (ModelState.IsValid) { var item = db.Customers.Find(customer.CustomerID); if (item == null) return HttpNotFound(); item.FullName = customer.FullName; item.Phone = customer.Phone; item.Email = customer.Email; item.Address = customer.Address; db.SaveChanges(); return RedirectToAction("Index"); } return View(customer); }
        public ActionResult Delete(int? id) { if (id == null) return new HttpStatusCodeResult(400); var item = db.Customers.Find(id); return item == null ? (ActionResult)HttpNotFound() : View(item); }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) { var item = db.Customers.Find(id); if (item == null) return HttpNotFound(); db.Customers.Remove(item); db.SaveChanges(); return RedirectToAction("Index"); }
        protected override void Dispose(bool disposing) { if (disposing) db.Dispose(); base.Dispose(disposing); }
    }
}
