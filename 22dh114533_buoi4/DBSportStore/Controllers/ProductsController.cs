using DBSportStore.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBSportStore.Controllers
{
    public class ProductsController : Controller
    {
        private const int MaxImageBytes = 2 * 1024 * 1024;
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly DBSportStoreEntities db = new DBSportStoreEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.Include(p => p.Category).OrderBy(p => p.ProductID).ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(400);
            Product product = db.Products.Include(p => p.Category).SingleOrDefault(p => p.ProductID == id);
            if (product == null) return HttpNotFound();
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            PopulateCategories();
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase imageFile)
        {
            if (imageFile == null || imageFile.ContentLength == 0)
                ModelState.AddModelError("imageFile", "Vui lòng chọn ảnh sản phẩm.");
            else
                ValidateImage(imageFile);

            if (ModelState.IsValid)
            {
                product.ImageUrl = SaveImage(imageFile);
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateCategories(product.CategoryID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(400);
            Product product = db.Products.Find(id);
            if (product == null) return HttpNotFound();
            PopulateCategories(product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product input, HttpPostedFileBase imageFile)
        {
            Product product = db.Products.Find(input.ProductID);
            if (product == null) return HttpNotFound();

            if (imageFile != null && imageFile.ContentLength > 0)
                ValidateImage(imageFile);

            if (ModelState.IsValid)
            {
                // Update only editable fields; keep the current image unless a new file was selected.
                product.ProductName = input.ProductName;
                product.Description = input.Description;
                product.CategoryID = input.CategoryID;
                product.Price = input.Price;
                product.StockQuantity = input.StockQuantity;
                product.IsActive = input.IsActive;
                if (imageFile != null && imageFile.ContentLength > 0)
                    product.ImageUrl = SaveImage(imageFile);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            input.ImageUrl = product.ImageUrl;
            PopulateCategories(input.CategoryID);
            return View(input);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(400);
            Product product = db.Products.Include(p => p.Category).SingleOrDefault(p => p.ProductID == id);
            if (product == null) return HttpNotFound();
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null) return HttpNotFound();
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void PopulateCategories(int? selectedCategoryId = null)
        {
            ViewBag.CategoryID = new SelectList(
                db.Categories.OrderBy(c => c.CategoryName),
                "CategoryID", "CategoryName", selectedCategoryId);
        }

        private void ValidateImage(HttpPostedFileBase imageFile)
        {
            string extension = Path.GetExtension(Path.GetFileName(imageFile.FileName));
            if (string.IsNullOrEmpty(extension) ||
                !AllowedImageExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                ModelState.AddModelError("imageFile", "Chỉ chấp nhận file JPG, JPEG hoặc PNG.");

            if (imageFile.ContentLength > MaxImageBytes)
                ModelState.AddModelError("imageFile", "Dung lượng ảnh tối đa là 2 MB.");
        }

        private string SaveImage(HttpPostedFileBase imageFile)
        {
            string originalName = Path.GetFileName(imageFile.FileName);
            string stem = Path.GetFileNameWithoutExtension(originalName);
            string extension = Path.GetExtension(originalName).ToLowerInvariant();
            string folder = Server.MapPath("~/Content/images");
            Directory.CreateDirectory(folder);

            string fileName = stem + extension;
            string path = Path.Combine(folder, fileName);
            int suffix = 1;
            while (System.IO.File.Exists(path))
            {
                fileName = stem + "-" + suffix + extension;
                path = Path.Combine(folder, fileName);
                suffix++;
            }

            imageFile.SaveAs(path);
            return fileName;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
