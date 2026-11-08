using DBSportStore.Models;
using System.Linq;
using System.Web.Mvc;

namespace DBSportStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DBSportStoreEntities db = new DBSportStoreEntities();
        public ActionResult Index() { return View(db.Categories.OrderBy(c => c.CategoryName).ToList()); }
        public ActionResult Details(int? id) { if (id == null) return new HttpStatusCodeResult(400); var item = db.Categories.Find(id); return item == null ? (ActionResult)HttpNotFound() : View(item); }
        public ActionResult Create() { return View(); }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Category category) { if (ModelState.IsValid) { db.Categories.Add(category); db.SaveChanges(); return RedirectToAction("Index"); } return View(category); }
        public ActionResult Edit(int? id) { if (id == null) return new HttpStatusCodeResult(400); var item = db.Categories.Find(id); return item == null ? (ActionResult)HttpNotFound() : View(item); }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Category category) { if (ModelState.IsValid) { var item = db.Categories.Find(category.CategoryID); if (item == null) return HttpNotFound(); item.CategoryName = category.CategoryName; db.SaveChanges(); return RedirectToAction("Index"); } return View(category); }
        public ActionResult Delete(int? id) { if (id == null) return new HttpStatusCodeResult(400); var item = db.Categories.Find(id); return item == null ? (ActionResult)HttpNotFound() : View(item); }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) { var item = db.Categories.Find(id); if (item == null) return HttpNotFound(); db.Categories.Remove(item); db.SaveChanges(); return RedirectToAction("Index"); }
        protected override void Dispose(bool disposing) { if (disposing) db.Dispose(); base.Dispose(disposing); }
    }
}
